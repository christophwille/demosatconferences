# Kubernetes-based Event Driven Autoscaling (KEDA)

When it comes to serverless frameworks on top of Kubernetes, that there is choice might even be an understatement. 
So where does KEDA fit in with its event driven autoscaling of workloads?

Unsurprisingly, KEDA does integrate with varying event sources and scalers to activate and deactivate a deployment 
to scale to and from zero on no events. Looking at the list of event sources might make it look strange that this 
project originated with Microsoft, however, there is one interesting specialization aside from its generic posture 
(being able to be used with any container or deployment): its native integration with Azure Functions, which is an 
open-source serverless programming model from Microsoft.

Given that KEDA runs both in the cloud and on the edge (aka on-premises), and it having no external dependencies makes 
it inherently interesting for hybrid scenarios. Add Azure Functions native integration to that picture, you can employ 
it to extend your serverless deployments from utilizing PaaS in the cloud to your own clusters - with no code changes.

[CNCV Meetup - KEDA](https://www.meetup.com/Cloud-Native-Computing-Vienna/events/265767535/)

## Links to GH / Articles

* [GitHub: KEDA main repository](https://github.com/kedacore/keda)
* [Announcing KEDA: bringing event-driven containers and functions to Kubernetes](https://cloudblogs.microsoft.com/opensource/2019/05/06/announcing-keda-kubernetes-event-driven-autoscaling-containers/)
* [Microsoft and Red Hat’s KEDA Brings Event-Driven Autoscaling to Kubernetes](https://thenewstack.io/microsoft-and-red-hats-keda-brings-event-driven-autoscaling-to-kubernetes/)
* [KEDA Up Close: Kubernetes-based, Event-Driven Autoscaling](https://medium.com/better-programming/keda-kubernetes-up-close-f47cdf43920b)

## Videos

* [Jeff Hollan, Serverless Kubernetes KEDA and Azure Functions - PRE14](https://www.youtube.com/watch?v=ujuWVs0A-KM)

## Advanced Samples

* [How to auto scale your Kubernetes apps with Prometheus and KEDA](https://dev.to/azure/how-to-auto-scale-your-kubernetes-apps-with-prometheus-and-keda-39km)
* [Deploy a Function App with KEDA ](https://www.pulumi.com/blog/deploy-a-function-app-with-keda/)
