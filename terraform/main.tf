provider "null" {}

resource "null_resource" "render_backend" {
  provisioner "local-exec" {
    command = <<EOT
      render services create \
        --type web_service \
        --plan free \
        --name bookchanger-backend \
        --env production \
        --region oregon \
        --build-command "dotnet publish -c Release -o out" \
        --start-command "dotnet BooksChanger.dll" \
        --env-vars TOKEN=${var.TOKEN},DB_CONNECTION=${var.DB_CONNECTION}
    EOT
  }
}
