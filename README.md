# PushEventClient
Simpel klient der illustrerer anvendelse af datafordlerens push hændelser.

Der køres et OData endpoint med et specifikt skema, som datafordeleren bruger til hændelser.

Data gemmes i en sqlite fil.

Oven på dette er en simpel Angular frontend som viser en liste over events modtaget.

## Kørsel
```dotnet run```

Der er også inkluderet launch filer til vscode.

## Opsætning
* Tjek at dit domæne peger på dette projekt (example.dk).
  * Default er port 5000 for http, og 5001 for https. Peg på Https.
* Tjek at abonnementerne i selvbetjeningen på datafordeleren peger på dit domæne.
  * Det kan betale sig at tilføje og fjerne dem, når man debugger.


## SSL
Der bruges en ```.pfx``` fil. Den kan genereres af openssl via følgene kommando:

```
openssl pkcs12 -export -out ssl2.pfx -inkey ???.key -in ???.pem
```

```*.key``` er SSL privatnøglen, og ```*.pem``` filen er certifikatfilen. 

### Todo
* Valider om data faktisk er fra datafordeleren
  * Brug Cert pinning til validering af OData endpointet
  * ip whitelist?