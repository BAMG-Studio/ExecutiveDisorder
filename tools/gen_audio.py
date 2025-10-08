#!/usr/bin/env python3
"""
Audio Asset Generator for Executive Disorder
Generates music, SFX, and voice lines
"""

import json
import os
import sys
from pathlib import Path
from typing import Dict, List

class AudioGenerator:
    def __init__(self, provider: str = "mock"):
        """provider: 'local' (AudioCraft), 'api' (ElevenLabs), or 'mock'"""
        self.provider = provider
        self.music_dir = Path("unity/Assets/Audio/Music/Generated")
        self.sfx_dir = Path("unity/Assets/Audio/SFX/Generated")
        self.voice_dir = Path("unity/Assets/Audio/Voice/Generated")
        
        for dir_path in [self.music_dir, self.sfx_dir, self.voice_dir]:
            dir_path.mkdir(parents=True, exist_ok=True)
    
    def generate_from_yaml(self, data_dir: str = "data"):
        """Generate audio from YAML content files"""
        print("🎵 Audio Generator")
        print("=" * 50)
        
        import glob
        yaml_files = glob.glob(f"{data_dir}/**/*.yaml", recursive=True)
        
        music_count = 0
        sfx_count = 0
        voice_count = 0
        
        for yaml_file in yaml_files:
            m, s, v = self.process_yaml(yaml_file)
            music_count += m
            sfx_count += s
            voice_count += v
        
        print()
        print(f"✅ Generated:")
        print(f"   🎼 {music_count} music tracks")
        print(f"   🔊 {sfx_count} sound effects")
        print(f"   🎤 {voice_count} voice lines")
    
    def process_yaml(self, filepath: str):
        """Extract audio requirements from YAML"""
        import yaml
        
        with open(filepath, 'r') as f:
            data = yaml.safe_load(f)
        
        content_id = data['id']
        music = sfx = voice = 0
        
        # Music theme
        if 'theme_music' in data:
            self.generate_music(content_id, data['theme_music'])
            music = 1
        
        # Sound effects
        if 'sfxKey' in data:
            self.generate_sfx(content_id, data['sfxKey'])
            sfx = 1
        
        # Voice lines
        if 'voLine' in data:
            voice_style = data.get('voiceStyle', 'neutral')
            self.generate_voice(content_id, data['voLine'], voice_style)
            voice = 1
        
        return music, sfx, voice
    
    def generate_music(self, content_id: str, theme: str):
        """Generate music track"""
        output_file = self.music_dir / f"{content_id}_theme.json"
        
        metadata = {
            "id": f"{content_id}_theme",
            "theme": theme,
            "duration": 120,  # 2 minutes loop
            "status": "placeholder",
            "path": f"unity/Assets/Audio/Music/Generated/{content_id}_theme.mp3",
            "provider": self.provider
        }
        
        with open(output_file, 'w') as f:
            json.dump(metadata, f, indent=2)
    
    def generate_sfx(self, content_id: str, sfx_key: str):
        """Generate sound effect"""
        output_file = self.sfx_dir / f"{content_id}_{sfx_key}.json"
        
        metadata = {
            "id": f"{content_id}_{sfx_key}",
            "sfxKey": sfx_key,
            "duration": 2,
            "status": "placeholder",
            "path": f"unity/Assets/Audio/SFX/Generated/{content_id}_{sfx_key}.wav",
            "provider": self.provider
        }
        
        with open(output_file, 'w') as f:
            json.dump(metadata, f, indent=2)
    
    def generate_voice(self, content_id: str, line: str, style: str):
        """Generate voice line"""
        output_file = self.voice_dir / f"{content_id}_vo.json"
        
        metadata = {
            "id": f"{content_id}_vo",
            "text": line,
            "style": style,
            "duration": len(line) * 0.1,  # Rough estimate
            "status": "placeholder",
            "path": f"unity/Assets/Audio/Voice/Generated/{content_id}_vo.mp3",
            "provider": self.provider
        }
        
        with open(output_file, 'w') as f:
            json.dump(metadata, f, indent=2)

def main():
    provider = os.getenv("AUDIO_PROVIDER", "mock")
    generator = AudioGenerator(provider)
    generator.generate_from_yaml()

if __name__ == "__main__":
    main()
