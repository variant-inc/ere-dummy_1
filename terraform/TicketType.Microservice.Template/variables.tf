// Variant Settings
// The following values come from deploy.sh & plan.sh
// in scripts/octo/
variable "deployable" {
  type=string
}

variable "environment" {
  type = string
}

variable "revision" {
  type = string
}

variable "image_tag" {
  type = string
}

variable "oidc_provider" {
  type = string
}
///

variable "k8s_serviceaccount" {
  type = string
}

variable "target_namespace" {
  type = string
}

variable "project_name" {
  type = string
}

variable "team" {
  type = string
}

variable "owner" {
  type = string
}

// AWS Settings
variable "aws_policy_version" {
  type = string
}

variable "aws_default_region" {
  type    = string
  default = "us-east-1"
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

// SNS/SQS
variable "entity_queue_retention_seconds" {
  type = number
  default     = 604800 # 7 days
  description = "Number of seconds the queue retains messages"
}

variable "entity_api_incoming_queue_name" {
  type = string
}

variable "entity_api_incoming_queue_url" {
  type = string
}

variable "ticketing_api_outgoing_topic_name" {
  type = string
}

variable "kms_key_alias_sns" {
  type    = string
  default = "alias/ops/sns"
}

// Data Sources
variable "enity_api_user_agent" {
  type = "string"
}

variable "entity_api_base_address" {
  type = "string"
}

variable "entity_api_driver_path" {
  type = "string"
}

variable "entity_api_hometime_path" {
  type = "string"
}

variable "entity_api_order_path" {
  type = "string"
}

variable "entity_api_tractor_path" {
  type = "string"
}