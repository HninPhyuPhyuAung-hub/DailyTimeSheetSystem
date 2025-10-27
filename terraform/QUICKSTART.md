# Quick Start Guide

This guide will help you quickly deploy the three-region VPC architecture.

## Prerequisites

- AWS Account with administrative access
- AWS CLI installed and configured
- Terraform >= 1.0 installed

## Quick Deploy (5 minutes)

### Step 1: Configure AWS Credentials

```bash
export AWS_ACCESS_KEY_ID="your-access-key-id"
export AWS_SECRET_ACCESS_KEY="your-secret-access-key"
```

Or use AWS CLI:
```bash
aws configure
```

### Step 2: Navigate to Terraform Directory

```bash
cd terraform
```

### Step 3: Initialize Terraform

```bash
terraform init
```

Expected output:
```
Initializing modules...
Initializing provider plugins...
Terraform has been successfully initialized!
```

### Step 4: Review the Plan

```bash
terraform plan
```

This shows what will be created:
- 3 VPCs
- 12 Subnets (6 public, 6 private)
- 3 Internet Gateways
- 3 NAT Gateways
- Route tables and security groups

### Step 5: Deploy Infrastructure

```bash
terraform apply
```

Type `yes` when prompted.

Deployment takes approximately 5-10 minutes.

### Step 6: View Outputs

```bash
terraform output
```

You'll see:
- VPC IDs for each region
- Subnet IDs
- NAT Gateway IDs and public IPs

## What Gets Created

### Singapore Region (ap-southeast-1)
```
VPC: 10.0.0.0/16
├── Public Subnets
│   ├── 10.0.1.0/24 (ap-southeast-1a)
│   └── 10.0.2.0/24 (ap-southeast-1b)
└── Private Subnets
    ├── 10.0.101.0/24 (ap-southeast-1a)
    └── 10.0.102.0/24 (ap-southeast-1b)
```

### South Korea Region (ap-northeast-2)
```
VPC: 10.1.0.0/16
├── Public Subnets
│   ├── 10.1.1.0/24 (ap-northeast-2a)
│   └── 10.1.2.0/24 (ap-northeast-2c)
└── Private Subnets
    ├── 10.1.101.0/24 (ap-northeast-2a)
    └── 10.1.102.0/24 (ap-northeast-2c)
```

### Japan Region (ap-northeast-1)
```
VPC: 10.2.0.0/16
├── Public Subnets
│   ├── 10.2.1.0/24 (ap-northeast-1a)
│   └── 10.2.2.0/24 (ap-northeast-1c)
└── Private Subnets
    ├── 10.2.101.0/24 (ap-northeast-1a)
    └── 10.2.102.0/24 (ap-northeast-1c)
```

## Verify Deployment

### Via Terraform
```bash
# List all created VPCs
terraform state list | grep vpc

# Show specific VPC details
terraform state show module.vpc_singapore.aws_vpc.main
```

### Via AWS CLI
```bash
# List VPCs in Singapore
aws ec2 describe-vpcs --region ap-southeast-1 \
  --filters "Name=tag:Region,Values=Singapore"

# List VPCs in Korea
aws ec2 describe-vpcs --region ap-northeast-2 \
  --filters "Name=tag:Region,Values=Korea"

# List VPCs in Japan
aws ec2 describe-vpcs --region ap-northeast-1 \
  --filters "Name=tag:Region,Values=Japan"
```

### Via AWS Console
1. Go to VPC Dashboard
2. Select region from dropdown
3. View VPCs, Subnets, Route Tables, etc.

## Testing Connectivity

### Deploy Test EC2 Instance (Optional)

Create `test-ec2.tf`:
```hcl
# Test EC2 instance in Singapore public subnet
resource "aws_instance" "test_singapore" {
  provider      = aws.singapore
  ami           = "ami-0c55b159cbfafe1f0"  # Amazon Linux 2 (update for your region)
  instance_type = "t2.micro"
  subnet_id     = module.vpc_singapore.public_subnet_ids[0]
  
  vpc_security_group_ids = [
    module.vpc_singapore.web_security_group_id
  ]
  
  tags = {
    Name = "test-singapore"
  }
}
```

Apply:
```bash
terraform apply
```

Connect via SSH:
```bash
ssh -i your-key.pem ec2-user@<public-ip>
```

## Cost Estimate

Approximate monthly costs:
- **NAT Gateway**: $32.40/gateway × 3 = ~$97.20/month
- **Elastic IP**: $0 (when attached)
- **Data Transfer**: Variable based on usage
- **Total**: ~$100-150/month

## Cleanup

To destroy all resources:

```bash
terraform destroy
```

Type `yes` when prompted.

⚠️ **Warning**: This will delete all created resources permanently.

## Troubleshooting

### Issue: Terraform init fails
**Solution**: Check internet connectivity and Terraform version
```bash
terraform version  # Should be >= 1.0
```

### Issue: AWS credentials error
**Solution**: Verify credentials are set correctly
```bash
aws sts get-caller-identity
```

### Issue: Insufficient permissions
**Solution**: Ensure IAM user/role has these permissions:
- ec2:*
- vpc:*
- route53:* (if using Route53)

### Issue: Resource limit exceeded
**Solution**: Request limit increase in AWS Service Quotas

### Issue: Already have VPCs in regions
**Solution**: Either delete existing VPCs or modify CIDR blocks in `main.tf`

## Next Steps

1. **Deploy Applications**: Add EC2 instances or ECS containers
2. **Add Load Balancing**: Deploy Application Load Balancers
3. **Setup Databases**: Deploy RDS in private subnets
4. **Configure Route53**: Enable geolocation routing
5. **Enable Monitoring**: Set up CloudWatch and VPC Flow Logs
6. **Implement Auto Scaling**: Add Auto Scaling Groups

## Additional Resources

- [Full Documentation](./README.md)
- [Architecture Details](./ARCHITECTURE.md)
- [AWS VPC Best Practices](https://docs.aws.amazon.com/vpc/latest/userguide/vpc-best-practices.html)
- [Terraform AWS Provider](https://registry.terraform.io/providers/hashicorp/aws/latest/docs)

## Support

For issues or questions:
1. Check the [README.md](./README.md)
2. Review [ARCHITECTURE.md](./ARCHITECTURE.md)
3. Check Terraform logs: `TF_LOG=DEBUG terraform apply`
4. Review AWS CloudTrail for API errors

---

**Deployment Time**: ~5-10 minutes  
**Estimated Cost**: ~$100-150/month  
**Difficulty**: Beginner to Intermediate
