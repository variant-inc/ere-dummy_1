// Create temporary incoming topic
data "aws_sns_topic" "incoming_exceptions_topic" {
  name = var.entity_api_incoming_topic
}
