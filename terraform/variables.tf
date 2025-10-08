variable "github_token" {
  description = "GitHub personal access token with repo and admin:org permissions"
  type        = string
  sensitive   = true
}

variable "github_owner" {
  description = "GitHub organization or user name"
  type        = string
  default     = "BAMG-Studio"
}

variable "unity_license" {
  description = "Unity license file content (base64 encoded)"
  type        = string
  sensitive   = true
  default     = ""
}

variable "unity_email" {
  description = "Unity account email"
  type        = string
  sensitive   = true
  default     = ""
}

variable "unity_password" {
  description = "Unity account password"
  type        = string
  sensitive   = true
  default     = ""
}

variable "openai_api_key" {
  description = "OpenAI API key for Codex CLI"
  type        = string
  sensitive   = true
  default     = ""
}

variable "custom_domain" {
  description = "Custom domain for GitHub Pages (optional)"
  type        = string
  default     = ""
}
