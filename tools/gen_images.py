#!/usr/bin/env python3
"""
Image Asset Generator for Executive Disorder  
Generates images via local Stable Diffusion or API
"""

import json
import os
import sys
from pathlib import Path
from typing import Dict, List

class ImageGenerator:
    def __init__(self, provider: str = "mock"):
        """provider: 'local' (ComfyUI), 'api' (DALL-E/SD), or 'mock' (placeholder)"""
        self.provider = provider
        self.output_dir = Path("unity/Assets/Art/Generated")
        self.output_dir.mkdir(parents=True, exist_ok=True)
        
        # Style presets for consistency
        self.styles = {
            "satirical_poster_v1": {
                "prompt_suffix": ", 1950s propaganda poster style, bold colors, dramatic composition",
                "negative": "realistic, photo, modern"
            },
            "professional_portrait": {
                "prompt_suffix": ", professional portrait photograph, oval office background",
                "negative": "cartoon, anime, painting"
            }
        }
    
    def generate_from_yaml(self, data_dir: str = "data"):
        """Generate images from YAML content files"""
        print("🎨 Image Generator")
        print("=" * 50)
        
        # Find all YAML files
        import glob
        yaml_files = glob.glob(f"{data_dir}/**/*.yaml", recursive=True)
        
        for yaml_file in yaml_files:
            self.process_yaml(yaml_file)
        
        print(f"✅ Generated {len(yaml_files)} image requests")
    
    def process_yaml(self, filepath: str):
        """Extract art prompts from YAML and generate"""
        import yaml
        
        with open(filepath, 'r') as f:
            data = yaml.safe_load(f)
        
        if 'artPrompt' in data:
            content_id = data['id']
            style = data.get('artStyle', 'satirical_poster_v1')
            prompt = data['artPrompt']
            
            print(f"  📝 {content_id}: {prompt[:60]}...")
            
            if self.provider == "mock":
                self.generate_placeholder(content_id, style)
            else:
                self.generate_real(content_id, style, prompt)
    
    def generate_placeholder(self, content_id: str, style: str):
        """Create placeholder image metadata"""
        output_file = self.output_dir / f"{content_id}.json"
        
        metadata = {
            "id": content_id,
            "style": style,
            "status": "placeholder",
            "path": f"unity/Assets/Art/Generated/{content_id}.png",
            "provider": self.provider
        }
        
        with open(output_file, 'w') as f:
            json.dump(metadata, f, indent=2)
    
    def generate_real(self, content_id: str, style: str, prompt: str):
        """TODO: Implement real generation"""
        print(f"    ⚠️  Real generation not implemented yet")
        self.generate_placeholder(content_id, style)

def main():
    provider = os.getenv("IMG_PROVIDER", "mock")
    generator = ImageGenerator(provider)
    generator.generate_from_yaml()

if __name__ == "__main__":
    main()
