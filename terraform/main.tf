terraform {
  required_version = ">= 1.0"
  
  required_providers {
    github = {
      source  = "integrations/github"
      version = "~> 6.0"
    }
  }
}

provider "github" {
  token = var.github_token
  owner = var.github_owner
}

# Enable GitHub Pages
resource "github_repository" "executive_disorder" {
  name        = "ExecutiveDisorder"
  description = "Executive Disorder - Political satire card game built with Unity"
  
  visibility = "public"
  
  has_issues      = true
  has_projects    = true
  has_wiki        = true
  has_downloads   = true
  
  allow_merge_commit     = true
  allow_squash_merge     = true
  allow_rebase_merge     = true
  delete_branch_on_merge = true
  
  # GitHub Pages configuration
  pages {
    source {
      branch = "gh-pages"
      path   = "/"
    }
    
    cname = var.custom_domain != "" ? var.custom_domain : null
  }
  
  # Topics/tags for discoverability
  topics = [
    "unity",
    "unity3d",
    "webgl",
    "game-development",
    "political-satire",
    "card-game",
    "ai-generated",
    "codex-cli"
  ]
}

# Branch protection for main
resource "github_branch_protection" "main" {
  repository_id = github_repository.executive_disorder.node_id
  pattern       = "main"
  
  required_status_checks {
    strict = true
    contexts = [
      "build-webgl",
      "validate-code"
    ]
  }
  
  required_pull_request_reviews {
    dismiss_stale_reviews           = true
    require_code_owner_reviews      = true
    required_approving_review_count = 1
  }
  
  enforce_admins = false
}

# Branch protection for codex-cli
resource "github_branch_protection" "codex_cli" {
  repository_id = github_repository.executive_disorder.node_id
  pattern       = "codex-cli"
  
  required_status_checks {
    strict = true
    contexts = [
      "build-webgl"
    ]
  }
}

# Create gh-pages branch
resource "github_branch" "gh_pages" {
  repository = github_repository.executive_disorder.name
  branch     = "gh-pages"
}

# Repository secrets for GitHub Actions
resource "github_actions_secret" "unity_license" {
  repository      = github_repository.executive_disorder.name
  secret_name     = "UNITY_LICENSE"
  plaintext_value = var.unity_license
}

resource "github_actions_secret" "unity_email" {
  repository      = github_repository.executive_disorder.name
  secret_name     = "UNITY_EMAIL"
  plaintext_value = var.unity_email
}

resource "github_actions_secret" "unity_password" {
  repository      = github_repository.executive_disorder.name
  secret_name     = "UNITY_PASSWORD"
  plaintext_value = var.unity_password
}

resource "github_actions_secret" "openai_api_key" {
  repository      = github_repository.executive_disorder.name
  secret_name     = "OPENAI_API_KEY"
  plaintext_value = var.openai_api_key
}

# Repository variables
resource "github_actions_variable" "unity_version" {
  repository    = github_repository.executive_disorder.name
  variable_name = "UNITY_VERSION"
  value         = "6000.2.6f2"
}

resource "github_actions_variable" "build_target" {
  repository    = github_repository.executive_disorder.name
  variable_name = "BUILD_TARGET"
  value         = "WebGL"
}

# Enable GitHub Actions
resource "github_repository" "enable_actions" {
  name = github_repository.executive_disorder.name
  
  security_and_analysis {
    secret_scanning {
      status = "enabled"
    }
    
    secret_scanning_push_protection {
      status = "enabled"
    }
  }
}

# Outputs
output "repository_url" {
  value = github_repository.executive_disorder.html_url
}

output "pages_url" {
  value = github_repository.executive_disorder.pages[0].html_url
}

output "clone_url" {
  value = github_repository.executive_disorder.http_clone_url
}
