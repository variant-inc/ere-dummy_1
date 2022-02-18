// Variant Settings
variable "environment" {
  type = string
}

variable "team" {
  type = string
}

variable "owner" {
  type = string
}

variable "revision" {
  type = string
}

// AWS Settings
variable "aws_default_region" {
  type    = string
  default = "us-east-1"
}

variable "aws_secret_name" {
  type = string
}

variable "aws_secret_resource_name" {
  type = string
}

variable "image_tag" {
  type = string
}

variable "target_namespace" {
  type = string
}

variable "oidc_provider" {
  type = string
}

variable "k8s_serviceaccount" {
  type = string
}

// Epsagon Settings
variable "epsagon_app_name" {
  type = string
}

variable "epsagon_token" {
  type        = string
  description = "Set via 'Set AWS credentials' Octopus Script Module"
}

// LaunchDarkly Settings
variable "launch_darkly_key" {
  type = string
}

variable "launch_darkly_user" {
  type = string
}

//

variable "octopus_tags" {
  type = map(string)
}

variable "entity_queue_retention_seconds" {
  type = number
}

variable "kms_key_alias_incoming_sns" {
  type    = string
  default = "alias/ops/sns"
}

variable "entity_api_incoming_topic" {
  type = string
}

variable "entity_api_incoming_queue" {
  type = string
}

variable "ticketing_api_outgoing_topic" {
  type = string
}