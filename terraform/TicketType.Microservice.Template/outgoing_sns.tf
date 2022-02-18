// Create outgoing topic
resource "aws_sns_topic" "outgoing_exceptions_topic" {
  name              = local.sns_ticketing_topic_name
  kms_master_key_id = data.aws_kms_alias.sns_key.id

}