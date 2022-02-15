module "tags" {
  source = "github.com/variant-inc/lazy-terraform//submodules/tags?ref=v1"

  user_tags = {
    team    = var.team
    purpose = "Ticket Type Microservice Template"
    owner   = var.owner
  }

  octopus_tags = var.octopus_tags
  name         = "Tags"
} 