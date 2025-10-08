# Terraform state configuration
# For team collaboration, use remote backend (e.g., Terraform Cloud or S3)

# Example: Local backend (default)
# terraform {
#   backend "local" {
#     path = "terraform.tfstate"
#   }
# }

# Example: Terraform Cloud (recommended for teams)
# terraform {
#   cloud {
#     organization = "BAMG-Studio"
#     
#     workspaces {
#       name = "executive-disorder"
#     }
#   }
# }

# Example: S3 backend
# terraform {
#   backend "s3" {
#     bucket = "bamg-studio-terraform-state"
#     key    = "executive-disorder/terraform.tfstate"
#     region = "us-east-1"
#   }
# }
