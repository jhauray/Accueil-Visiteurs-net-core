# Accueil Visiteurs

Il s'agit d'une application exemple, destinée à tester et à démontrer l'interêt d'une technologie.


## Objectif

"Accueil Visiteurs" rempli les mêmes fonctions qu'un "Hello World" :
- Valider que l'infrastructure est fonctionnelle.
- Donner un exemple de code compatible aux développeurs.

## Caractéristiques techniques du projet
- Application [.Net Core 2.1](https://docs.microsoft.com/fr-fr/dotnet/core/)
- Backend [ASP.Net Core 2.1](https://docs.microsoft.com/fr-fr/aspnet/core/?view=aspnetcore-2.1)
- Persistance de données [Entity Framework Core 2.1](https://docs.microsoft.com/fr-fr/ef/core/)
- Gestion schéma BDD [Entity Framework Core Migration](https://docs.microsoft.com/fr-fr/ef/core/managing-schemas/migrations/)
- Tests unitaires [xunit 2.3.1](https://xunit.github.io/)
- Mocking [moq 4.x](https://github.com/moq/moq4)

dotnet add package Microsoft.AspNetCore.Mvc.Versioning --version 2.3.0
https://github.com/Microsoft/aspnet-api-versioning

## Fonctionnel

Cette application web, mono-page, va accueillir les visiteurs qui s'y connecte. Elle leur dira bonjour, et leur demandera comment ils s'appellent.

Une fois le nom des visiteurs enregistrés, l’application leur souhaitera la bienvenue, et leur affichera la liste des autres visiteurs de la journée.

## Développement
Pour générer la solution, utiliser la commande :
```
dotnet build
```
Note : Depuis .Net Core 2.0, la commande dotnet build appelle implicitement dotnet restore.

## Lancement
Pour exécuter la solution : 
```
dotnet run 
```

## Tests Unitaires
Pour exécuter les tests :
```
dotnet test
```