terraform {
    source = "tfr://registry.terraform.io/terraform-aws-modules/iam/aws//modules/iam-github-oidc-role/?version=5.39.0"
}

include "root" {
  path = find_in_parent_folders()
}

inputs = {
  name = "Admin"
  subjects = ["CSharpLevelUp/CodeTextToSpeech:*"]

  policies = {
    AdminAccess = "arn:aws:iam::aws:policy/AdministratorAccess"
  }
}
