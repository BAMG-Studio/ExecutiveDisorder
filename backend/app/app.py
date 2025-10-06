from flask import Flask, jsonify, request
from flask_cors import CORS
import psycopg2
from psycopg2.extras import RealDictCursor
import os
from datetime import datetime
import hashlib
import openai
import requests

app = Flask(__name__)
CORS(app)

DB_CONFIG = {
    'host': os.getenv('DB_HOST', 'localhost'),
    'database': os.getenv('DB_NAME', 'executive_disorder'),
    'user': os.getenv('DB_USER', 'postgres'),
    'password': os.getenv('DB_PASSWORD', 'postgres'),
    'port': os.getenv('DB_PORT', '5432')
}

def get_db():
    return psycopg2.connect(**DB_CONFIG, cursor_factory=RealDictCursor)

@app.route('/health', methods=['GET'])
def health():
    return jsonify({'status': 'ok', 'timestamp': datetime.utcnow().isoformat()})

@app.route('/api/users', methods=['POST'])
def create_user():
    data = request.json
    username = data.get('username')
    password_hash = hashlib.sha256(data.get('password').encode()).hexdigest()
    
    with get_db() as conn, conn.cursor() as cur:
        cur.execute(
            "INSERT INTO users (username, password_hash) VALUES (%s, %s) RETURNING id, username",
            (username, password_hash)
        )
        user = cur.fetchone()
        conn.commit()
    return jsonify(user), 201

@app.route('/api/saves', methods=['POST'])
def save_game():
    data = request.json
    with get_db() as conn, conn.cursor() as cur:
        cur.execute(
            "INSERT INTO game_saves (user_id, save_data) VALUES (%s, %s) RETURNING id",
            (data['user_id'], data['save_data'])
        )
        save_id = cur.fetchone()['id']
        conn.commit()
    return jsonify({'id': save_id}), 201

@app.route('/api/saves/<int:user_id>', methods=['GET'])
def get_saves(user_id):
    with get_db() as conn, conn.cursor() as cur:
        cur.execute("SELECT * FROM game_saves WHERE user_id = %s ORDER BY created_at DESC", (user_id,))
        saves = cur.fetchall()
    return jsonify(saves)

# AI Content Generation Endpoints

@app.route('/api/ai/generate-cards', methods=['POST'])
def generate_cards():
    """Generate decision cards using OpenAI GPT"""
    data = request.json
    theme = data.get('theme', 'General Policy')
    count = data.get('count', 1)
    
    openai_key = os.getenv('OPENAI_API_KEY')
    if not openai_key:
        return jsonify({'error': 'OpenAI API key not configured'}), 500
    
    try:
        openai.api_key = openai_key
        
        prompt = f"""Generate {count} decision cards for Executive Disorder, a political simulation game.
        
Theme: {theme}

For each card, provide:
1. Title (10-15 words)
2. Description (30-50 words)
3. 2-3 choices with consequences for: Popularity, Stability, MediaTrust, EconomicHealth (values -20 to +20)

Format as JSON array."""
        
        response = openai.ChatCompletion.create(
            model="gpt-4",
            messages=[
                {"role": "system", "content": "You are a creative writer for a political simulation game. Generate balanced, realistic decision cards."},
                {"role": "user", "content": prompt}
            ],
            max_tokens=1500,
            temperature=0.8
        )
        
        generated_content = response.choices[0].message.content
        return jsonify({'cards': generated_content, 'count': count}), 200
        
    except Exception as e:
        return jsonify({'error': str(e)}), 500

@app.route('/api/ai/generate-image', methods=['POST'])
def generate_image():
    """Generate images using DALL-E 3"""
    data = request.json
    prompt = data.get('prompt', '')
    
    openai_key = os.getenv('OPENAI_API_KEY')
    if not openai_key:
        return jsonify({'error': 'OpenAI API key not configured'}), 500
    
    try:
        openai.api_key = openai_key
        
        response = openai.Image.create(
            model="dall-e-3",
            prompt=prompt,
            size="1024x1024",
            quality="standard",
            n=1
        )
        
        image_url = response.data[0].url
        return jsonify({'image_url': image_url}), 200
        
    except Exception as e:
        return jsonify({'error': str(e)}), 500

@app.route('/api/ai/test-balance', methods=['POST'])
def test_balance():
    """Endpoint for ML-Agents to submit test results"""
    data = request.json
    test_results = data.get('results', [])
    
    # Analyze results and provide balance suggestions
    analysis = {
        'total_tests': len(test_results),
        'avg_survival_days': sum(r.get('days_survived', 0) for r in test_results) / max(len(test_results), 1),
        'win_rate': sum(1 for r in test_results if r.get('won', False)) / max(len(test_results), 1),
        'suggestions': []
    }
    
    # Add balance suggestions based on results
    if analysis['win_rate'] < 0.3:
        analysis['suggestions'].append('Game is too difficult - consider reducing negative consequences')
    elif analysis['win_rate'] > 0.7:
        analysis['suggestions'].append('Game is too easy - consider increasing challenge')
    
    return jsonify(analysis), 200

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=8000, debug=True)
