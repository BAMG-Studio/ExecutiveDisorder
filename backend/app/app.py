from flask import Flask, jsonify, request
from flask_cors import CORS
import psycopg2
from psycopg2.extras import RealDictCursor
import os
from datetime import datetime
import hashlib

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

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=8000, debug=True)
