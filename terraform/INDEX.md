# Documentation Index

Welcome to the Three-Region VPC Architecture documentation. This index will help you find the information you need.

## Quick Navigation

### 🚀 Getting Started
- **[QUICKSTART.md](QUICKSTART.md)** - Start here! Quick 5-minute deployment guide
- **[DEPLOYMENT_CHECKLIST.md](DEPLOYMENT_CHECKLIST.md)** - Step-by-step deployment checklist

### 📚 Core Documentation
- **[README.md](README.md)** - Comprehensive guide with full details
- **[ARCHITECTURE.md](ARCHITECTURE.md)** - Detailed technical architecture
- **[INFRASTRUCTURE_SUMMARY.md](INFRASTRUCTURE_SUMMARY.md)** - Complete resource inventory

### 📊 Visual Resources
- **[DIAGRAMS.md](DIAGRAMS.md)** - Network diagrams and traffic flows

### ⚙️ Configuration Files
- **[main.tf](main.tf)** - Main Terraform configuration
- **[variables.tf](variables.tf)** - Variable definitions
- **[outputs.tf](outputs.tf)** - Output definitions
- **[route53.tf](route53.tf)** - Route53 configuration template
- **[terraform.tfvars.example](terraform.tfvars.example)** - Example variables

### 📦 Modules
- **[modules/vpc/](modules/vpc/)** - Reusable VPC module
  - [main.tf](modules/vpc/main.tf) - VPC resources
  - [variables.tf](modules/vpc/variables.tf) - VPC variables
  - [outputs.tf](modules/vpc/outputs.tf) - VPC outputs

## Documentation by Use Case

### I want to deploy the infrastructure
1. Start with [QUICKSTART.md](QUICKSTART.md)
2. Follow [DEPLOYMENT_CHECKLIST.md](DEPLOYMENT_CHECKLIST.md)
3. Reference [README.md](README.md) for detailed instructions

### I want to understand the architecture
1. Read [ARCHITECTURE.md](ARCHITECTURE.md)
2. View [DIAGRAMS.md](DIAGRAMS.md) for visual representation
3. Check [INFRASTRUCTURE_SUMMARY.md](INFRASTRUCTURE_SUMMARY.md) for resource details

### I want to customize the deployment
1. Review [README.md](README.md) for customization options
2. Edit [variables.tf](variables.tf) for global settings
3. Modify [main.tf](main.tf) for region-specific settings
4. Check [modules/vpc/](modules/vpc/) to customize VPC module

### I want to add Route53 geolocation routing
1. Read Route53 section in [README.md](README.md)
2. Review [route53.tf](route53.tf) template
3. Follow instructions in [ARCHITECTURE.md](ARCHITECTURE.md)

### I want to troubleshoot issues
1. Check Troubleshooting section in [README.md](README.md)
2. Review [DEPLOYMENT_CHECKLIST.md](DEPLOYMENT_CHECKLIST.md)
3. Verify configuration against [ARCHITECTURE.md](ARCHITECTURE.md)

## Documentation by Topic

### Architecture & Design
- [ARCHITECTURE.md](ARCHITECTURE.md) - Complete architecture details
- [DIAGRAMS.md](DIAGRAMS.md) - Visual diagrams
- [INFRASTRUCTURE_SUMMARY.md](INFRASTRUCTURE_SUMMARY.md) - Resource summary

### Deployment & Operations
- [QUICKSTART.md](QUICKSTART.md) - Quick deployment
- [DEPLOYMENT_CHECKLIST.md](DEPLOYMENT_CHECKLIST.md) - Detailed checklist
- [README.md](README.md) - Complete operations guide

### Configuration
- [main.tf](main.tf) - Main configuration
- [variables.tf](variables.tf) - Variables
- [outputs.tf](outputs.tf) - Outputs
- [terraform.tfvars.example](terraform.tfvars.example) - Example values

### Networking
- VPC Configuration: [modules/vpc/main.tf](modules/vpc/main.tf)
- Subnets: Public & Private (2 each per region)
- Routing: IGW and NAT Gateway configurations
- Security: Security Groups for web and database tiers

### Advanced Features
- [route53.tf](route53.tf) - Geolocation routing template
- Multi-region deployment patterns
- High availability configurations

## Key Concepts

### Regions Covered
- **Singapore** (ap-southeast-1) - VPC 10.0.0.0/16
- **South Korea** (ap-northeast-2) - VPC 10.1.0.0/16
- **Japan** (ap-northeast-1) - VPC 10.2.0.0/16

### Resources Per Region
- 1 VPC
- 2 Public Subnets (different AZs)
- 2 Private Subnets (different AZs)
- 1 Internet Gateway
- 1 NAT Gateway + Elastic IP
- 2 Route Tables
- 2 Security Groups (Web + Database)

### Total Resources
- 3 VPCs
- 12 Subnets
- 3 Internet Gateways
- 3 NAT Gateways
- 6 Route Tables
- 6 Security Groups

## File Sizes & Reading Time

| Document | Size | Est. Reading Time |
|----------|------|-------------------|
| QUICKSTART.md | 5.5 KB | 5 minutes |
| README.md | 9.5 KB | 10-15 minutes |
| ARCHITECTURE.md | 7 KB | 10 minutes |
| INFRASTRUCTURE_SUMMARY.md | 9.6 KB | 10-15 minutes |
| DIAGRAMS.md | 15 KB | 5 minutes (visual) |
| DEPLOYMENT_CHECKLIST.md | 9.8 KB | Reference |

## Recommended Reading Order

### For First-Time Users
1. **[QUICKSTART.md](QUICKSTART.md)** - Get oriented (5 min)
2. **[DIAGRAMS.md](DIAGRAMS.md)** - Understand visually (5 min)
3. **[DEPLOYMENT_CHECKLIST.md](DEPLOYMENT_CHECKLIST.md)** - Follow along (20-30 min)
4. **[README.md](README.md)** - Deep dive when needed

### For Experienced Users
1. **[ARCHITECTURE.md](ARCHITECTURE.md)** - Technical specs
2. **[main.tf](main.tf)** - Review configuration
3. **[QUICKSTART.md](QUICKSTART.md)** - Deploy quickly

### For Architects
1. **[ARCHITECTURE.md](ARCHITECTURE.md)** - Design patterns
2. **[DIAGRAMS.md](DIAGRAMS.md)** - Network topology
3. **[INFRASTRUCTURE_SUMMARY.md](INFRASTRUCTURE_SUMMARY.md)** - Resource inventory
4. **[modules/vpc/](modules/vpc/)** - Module design

## External References

- [AWS VPC Documentation](https://docs.aws.amazon.com/vpc/)
- [Route53 Geolocation Routing](https://docs.aws.amazon.com/Route53/latest/DeveloperGuide/routing-policy-geo.html)
- [Terraform AWS Provider](https://registry.terraform.io/providers/hashicorp/aws/latest/docs)
- [Lab Reference](https://vcloudynet.blogspot.com/2017/02/lab-route53-geolocation-routing-policy.html?m=1)

## Support & Contribution

### Getting Help
1. Check relevant documentation above
2. Review troubleshooting sections
3. Check Terraform error messages
4. Review AWS CloudTrail logs

### Documentation Updates
If you find errors or have suggestions:
1. Note the document name and section
2. Describe the issue or improvement
3. Submit feedback to repository maintainers

## Version Information

- **Documentation Version**: 1.0
- **Last Updated**: 2025-10-27
- **Terraform Version**: >= 1.0
- **AWS Provider**: ~> 5.0

## Quick Commands Reference

```bash
# Initialize Terraform
terraform init

# Validate configuration
terraform validate

# Format code
terraform fmt

# Plan deployment
terraform plan

# Deploy infrastructure
terraform apply

# Show outputs
terraform output

# Destroy infrastructure
terraform destroy
```

## Directory Structure

```
terraform/
├── INDEX.md                     # This file
├── README.md                    # Main documentation
├── QUICKSTART.md                # Quick start guide
├── ARCHITECTURE.md              # Architecture details
├── INFRASTRUCTURE_SUMMARY.md    # Resource summary
├── DIAGRAMS.md                  # Visual diagrams
├── DEPLOYMENT_CHECKLIST.md      # Deployment checklist
├── main.tf                      # Main configuration
├── variables.tf                 # Variables
├── outputs.tf                   # Outputs
├── route53.tf                   # Route53 template
├── terraform.tfvars.example     # Example variables
├── .gitignore                   # Git ignore rules
└── modules/
    └── vpc/
        ├── main.tf              # VPC resources
        ├── variables.tf         # VPC variables
        └── outputs.tf           # VPC outputs
```

---

**Need Help?** Start with [QUICKSTART.md](QUICKSTART.md) or check the relevant section above.

**Ready to Deploy?** Follow [DEPLOYMENT_CHECKLIST.md](DEPLOYMENT_CHECKLIST.md).

**Want Details?** Read [README.md](README.md) and [ARCHITECTURE.md](ARCHITECTURE.md).
