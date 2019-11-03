az group create --name cwirgkedademovie --location westeurope

az acr create --resource-group cwirgkedademovie --name cwiacrkedademovie --sku Basic

az acr login --name cwiacrkedademovie

az aks create \
    --resource-group cwirgkedademovie \
    --name cwiaksclusterdemovie \
    --node-count 2 \
    --generate-ssh-keys \
    --attach-acr cwiacrkedademovie

az aks get-credentials --resource-group cwirgkedademovie --name cwiaksclusterdemovie

kubectl get nodes