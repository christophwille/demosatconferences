# Gestatten, Bond. Es muss nicht immer JSON sein

Das Ziel jeder Softwarelösung ist es, Elektrizität in Einnahmen umzuwandeln, nicht aber nur in Abwärme. Trotzdem scheinen viele Entwickler sich eher an das Lied "Wer soll das bezahlen?" zu halten - denn sie verwenden REST und JSON (Cloud-Provider *lieben* JSON). Provokant? Ja. Aber wieder einmal hinken wir in der .NET Welt dem allgemeinen Trend hinterher - "alle" anderen diskutieren schon lange über Sinn und Unsinn von JSON, vor allem (aber nicht nur) wenn es um Microservices geht.

Wir sehen uns die technischen Hintergründe an (CPU und Memory) warum JSON nicht immer die beste Wahl ist, und vor allem - was ist denn der Ersatz, und in welchen Szenarien macht das Sinn? Wir tauchen ein in die Welt von gRPC und Serialisierungsformaten (Bond als ein OSS Vertreter direkt von Microsoft) und wo/wie man diese in die eigene Lösung einbauen kann - und vor allem sollte.

Motivation:

 * [Kelsey Hightower - Building Microservices with gRPC and Kubernetes](http://www.ustream.tv/recorded/86187859)
 * [Dan North: Decisions, decisions](http://www.ustream.tv/recorded/102892648 )

Resources:

 * [Bond repo](https://github.com/Microsoft/bond)
 * [Schema evolution in Avro, Protocol Buffers and Thrift](https://martin.kleppmann.com/2012/12/05/schema-evolution-in-avro-protocol-buffers-thrift.html)
 * [Samples von Raphael Schwarz' GAB 2017 Talk](https://github.com/schwarzr/Samples/tree/master/Azure/LargeData)
 * [Deren Liao - gRPC: Efficient RPC framework for .NET microservices](https://www.youtube.com/watch?v=kJiHH1x53MM)
 * [Best Practices for (Go) gRPC Services ](https://www.youtube.com/watch?v=Z_yD7YPL2oE)
 * [Comparison of Microsoft.Bond and Google Protocol Buffers performance](https://github.com/takemyoxygen/bond-performance-tests) 
 * [Formatter collection for running ASP.NET Core applications](https://github.com/jterry75/Bond-ASP.NET-Core-Formatters)
