#!/usr/bin/env python3
"""
Generate AI portraits for all 8 Executive Disorder characters
Requires: Flask backend running with DALL-E 3 integration
"""

import requests
import json
import time
from pathlib import Path

# Configuration
API_BASE_URL = "https://your-replit-url.repl.co"  # UPDATE THIS
OUTPUT_DIR = Path("../unity/Assets/Art/Characters/Generated")
OUTPUT_DIR.mkdir(parents=True, exist_ok=True)

# Character portrait specifications
CHARACTERS = [
    {
        "name": "Rex Scaleston III",
        "filename": "RexScalestonPortrait.png",
        "prompt": "Professional political portrait of an iguana wearing a business suit and red tie, presidential, dignified, photorealistic, studio lighting, official government portrait style, 4K quality"
    },
    {
        "name": "Donald J. Executive",
        "filename": "DonaldExecutivePortrait.png",
        "prompt": "Professional portrait of a confident business executive in expensive navy suit, orange tan, distinctive blonde hairstyle, presidential bearing, studio lighting, official portrait style, 4K quality"
    },
    {
        "name": "POTUS-9000",
        "filename": "POTUS9000Portrait.png",
        "prompt": "Futuristic AI robot president, sleek metallic design with chrome finish, glowing blue eyes, wearing presidential suit and tie, professional portrait, sci-fi, high-tech, studio lighting, 4K quality"
    },
    {
        "name": "Alexandria Sanders-Warren",
        "filename": "AlexandriaPortrait.png",
        "prompt": "Young progressive female politician, professional portrait, confident smile, modern business attire, diverse, inspiring, studio lighting, official government portrait style, 4K quality"
    },
    {
        "name": "Richard M. Moneybags III",
        "filename": "RichardMoneybagsPortrait.png",
        "prompt": "Wealthy aristocratic businessman, expensive three-piece suit, monocle, smug expression, old money aesthetic, oil painting style portrait, luxurious, studio lighting, 4K quality"
    },
    {
        "name": "General James Ironside Steel",
        "filename": "GeneralSteelPortrait.png",
        "prompt": "Stern military general in dress uniform with medals, crew cut, strong jaw, authoritative presence, professional military portrait, American flag background, studio lighting, 4K quality"
    },
    {
        "name": "Diana Newsworthy",
        "filename": "DianaNewsworthyPortrait.png",
        "prompt": "Sophisticated female media executive, power suit, confident pose, studio lighting, professional glamour shot, influential, modern, official portrait style, 4K quality"
    },
    {
        "name": "Johnny Q. Public",
        "filename": "JohnnyPublicPortrait.png",
        "prompt": "Average middle-aged man in casual business attire, friendly smile, relatable, working class hero, approachable, everyman, studio lighting, official portrait style, 4K quality"
    }
]

def generate_portrait(character):
    """Generate a single character portrait"""
    print(f"\n{'='*60}")
    print(f"Generating portrait for: {character['name']}")
    print(f"{'='*60}")
    
    # Prepare request
    url = f"{API_BASE_URL}/api/generate-character-portrait"
    payload = {
        "character_name": character["name"],
        "description": character["prompt"]
    }
    
    print(f"Sending request to: {url}")
    print(f"Prompt: {character['prompt'][:100]}...")
    
    try:
        # Send request
        response = requests.post(
            url,
            json=payload,
            headers={"Content-Type": "application/json"},
            timeout=60
        )
        
        if response.status_code == 200:
            # Save image
            output_path = OUTPUT_DIR / character["filename"]
            with open(output_path, 'wb') as f:
                f.write(response.content)
            
            print(f"✅ SUCCESS: Portrait saved to {output_path}")
            return True
        else:
            print(f"❌ ERROR: Status {response.status_code}")
            print(f"Response: {response.text[:200]}")
            return False
            
    except requests.exceptions.Timeout:
        print(f"❌ ERROR: Request timed out (60s)")
        return False
    except Exception as e:
        print(f"❌ ERROR: {str(e)}")
        return False

def main():
    print("="*60)
    print("Executive Disorder - Character Portrait Generator")
    print("="*60)
    print(f"API Base URL: {API_BASE_URL}")
    print(f"Output Directory: {OUTPUT_DIR}")
    print(f"Characters to generate: {len(CHARACTERS)}")
    print("="*60)
    
    # Check backend health
    print("\nChecking backend health...")
    try:
        health_response = requests.get(f"{API_BASE_URL}/health", timeout=10)
        if health_response.status_code == 200:
            print("✅ Backend is healthy")
        else:
            print("⚠️  Backend health check failed")
            print("Continuing anyway...")
    except Exception as e:
        print(f"⚠️  Could not reach backend: {e}")
        print("Please ensure your Flask backend is running!")
        return
    
    # Generate portraits
    results = []
    for i, character in enumerate(CHARACTERS, 1):
        print(f"\n[{i}/{len(CHARACTERS)}] Processing {character['name']}...")
        success = generate_portrait(character)
        results.append({
            "name": character["name"],
            "success": success,
            "filename": character["filename"]
        })
        
        # Wait between requests to avoid rate limiting
        if i < len(CHARACTERS):
            print("\nWaiting 5 seconds before next request...")
            time.sleep(5)
    
    # Summary
    print("\n" + "="*60)
    print("GENERATION SUMMARY")
    print("="*60)
    
    successful = sum(1 for r in results if r["success"])
    failed = len(results) - successful
    
    print(f"\nTotal: {len(results)}")
    print(f"✅ Successful: {successful}")
    print(f"❌ Failed: {failed}")
    
    if successful > 0:
        print("\n✅ Successfully generated:")
        for r in results:
            if r["success"]:
                print(f"   - {r['name']} → {r['filename']}")
    
    if failed > 0:
        print("\n❌ Failed to generate:")
        for r in results:
            if not r["success"]:
                print(f"   - {r['name']}")
    
    print("\n" + "="*60)
    print("Next steps:")
    print("1. Import generated images into Unity")
    print("2. Set Texture Type to 'Sprite (2D and UI)'")
    print("3. Assign to character ScriptableObjects")
    print("="*60)

if __name__ == "__main__":
    main()
