# https://github.com/kedacore/sample-dotnet-worker-servicebus-queue

docker build . --tag keda-sample-dotnet-worker-servicebus-queue --file ./Keda.Samples.Dotnet.OrderProcessor/Dockerfile --no-cache

az acr login --name cwiacrkedademovie
az acr list --resource-group cwirgkedademovie --query "[].{acrLoginServer:loginServer}" --output table

docker tag keda-sample-dotnet-worker-servicebus-queue cwiacrkedademovie.azurecr.io/keda-sample-dotnet-worker-servicebus-queue:v1
docker push cwiacrkedademovie.azurecr.io/keda-sample-dotnet-worker-servicebus-queue:v1
az acr repository list --name cwiacrkedademovie --output table