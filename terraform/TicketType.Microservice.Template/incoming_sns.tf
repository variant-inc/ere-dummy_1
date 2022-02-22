// Create temporary incoming topic
resource "aws_sns_topic" "incoming_entity_topic" {
  name              = local.sns_incoming_topic_name
  kms_master_key_id = data.aws_kms_alias.sns_key.id
}

// This stays
data "aws_sns_topic" "entity_api_topic" {
  name = local.sns_incoming_topic_name
}