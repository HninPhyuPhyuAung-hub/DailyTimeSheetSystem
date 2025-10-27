# Route53 Geolocation Routing Configuration
# This configuration sets up Route53 with geolocation-based routing for the three regions

# Note: This requires a valid domain name to be configured
# Uncomment and configure when you have a domain name

# # Route53 Hosted Zone (if creating a new one)
# resource "aws_route53_zone" "main" {
#   name = var.domain_name
#   
#   tags = {
#     Environment = var.environment
#     Name        = "${var.project_name}-hosted-zone"
#   }
# }

# # Health Checks for each region
# resource "aws_route53_health_check" "singapore" {
#   provider          = aws.us-east-1
#   type              = "HTTP"
#   resource_path     = "/health"
#   failure_threshold = "3"
#   request_interval  = "30"
#   
#   # Update with actual IP/endpoint after EC2 instances are created
#   # ip_address = module.vpc_singapore.nat_gateway_public_ip
#   
#   tags = {
#     Name = "singapore-health-check"
#   }
# }

# resource "aws_route53_health_check" "korea" {
#   provider          = aws.us-east-1
#   type              = "HTTP"
#   resource_path     = "/health"
#   failure_threshold = "3"
#   request_interval  = "30"
#   
#   # Update with actual IP/endpoint after EC2 instances are created
#   # ip_address = module.vpc_korea.nat_gateway_public_ip
#   
#   tags = {
#     Name = "korea-health-check"
#   }
# }

# resource "aws_route53_health_check" "japan" {
#   provider          = aws.us-east-1
#   type              = "HTTP"
#   resource_path     = "/health"
#   failure_threshold = "3"
#   request_interval  = "30"
#   
#   # Update with actual IP/endpoint after EC2 instances are created
#   # ip_address = module.vpc_japan.nat_gateway_public_ip
#   
#   tags = {
#     Name = "japan-health-check"
#   }
# }

# # Geolocation Records
# # Singapore - Default for Southeast Asia
# resource "aws_route53_record" "singapore" {
#   provider = aws.us-east-1
#   zone_id  = aws_route53_zone.main.zone_id
#   name     = var.domain_name
#   type     = "A"
#   ttl      = "60"
#   
#   # Update with actual IP after EC2 instances are created
#   records = [module.vpc_singapore.nat_gateway_public_ip]
#   
#   geolocation_routing_policy {
#     continent = "AS"
#     country   = "SG"
#   }
#   
#   set_identifier  = "singapore"
#   health_check_id = aws_route53_health_check.singapore.id
# }

# # Korea - Default for Korea
# resource "aws_route53_record" "korea" {
#   provider = aws.us-east-1
#   zone_id  = aws_route53_zone.main.zone_id
#   name     = var.domain_name
#   type     = "A"
#   ttl      = "60"
#   
#   # Update with actual IP after EC2 instances are created
#   records = [module.vpc_korea.nat_gateway_public_ip]
#   
#   geolocation_routing_policy {
#     continent = "AS"
#     country   = "KR"
#   }
#   
#   set_identifier  = "korea"
#   health_check_id = aws_route53_health_check.korea.id
# }

# # Japan - Default for Japan
# resource "aws_route53_record" "japan" {
#   provider = aws.us-east-1
#   zone_id  = aws_route53_zone.main.zone_id
#   name     = var.domain_name
#   type     = "A"
#   ttl      = "60"
#   
#   # Update with actual IP after EC2 instances are created
#   records = [module.vpc_japan.nat_gateway_public_ip]
#   
#   geolocation_routing_policy {
#     continent = "AS"
#     country   = "JP"
#   }
#   
#   set_identifier  = "japan"
#   health_check_id = aws_route53_health_check.japan.id
# }

# # Default record (fallback)
# resource "aws_route53_record" "default" {
#   provider = aws.us-east-1
#   zone_id  = aws_route53_zone.main.zone_id
#   name     = var.domain_name
#   type     = "A"
#   ttl      = "60"
#   
#   # Default to Singapore
#   records = [module.vpc_singapore.nat_gateway_public_ip]
#   
#   geolocation_routing_policy {
#     continent = "*"
#   }
#   
#   set_identifier  = "default"
#   health_check_id = aws_route53_health_check.singapore.id
# }
