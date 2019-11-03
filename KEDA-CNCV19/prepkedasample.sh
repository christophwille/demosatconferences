# https://github.com/kedacore/sample-dotnet-worker-servicebus-queue

az servicebus namespace create --name cwisbkedademovie --resource-group cwirgkedademovie --sku basic

az servicebus queue create --namespace-name cwisbkedademovie --name orders --resource-group cwirgkedademovie

az servicebus queue authorization-rule create --resource-group cwirgkedademovie --namespace-name cwisbkedademovie --queue-name orders --name order-consumer --rights Manage Send Listen

az servicebus queue authorization-rule keys list --resource-group cwirgkedademovie --namespace-name cwisbkedademovie --queue-name orders --name order-consumer
echo '<connection string - manual step>' | base64

kubectl create namespace keda-dotnet-sample

kubectl apply -f deploy/deploy-secret.yaml --namespace keda-dotnet-sample
kubectl get secrets --namespace keda-dotnet-sample

kubectl apply -f deploy/deploy-queue-processor.yaml --namespace keda-dotnet-sample