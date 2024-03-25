# <span style="color:Darkblue;">Fonctionnement de L'architecture D.D.D (Domain-Driver Design) pour une API</span>

# 🛜  <span style="color:dodgerblue;">API</span> 🛜
##### Contient les composants qui exposent les fonctionnalités de l'application au monde extérieur sous forme d'API ( En mono-project ).

### 📁 <span style="color: deepskyblue;">Controller</span>
Définit des contrôleurs qui s'occupent de la réception des requêtes HTTP.

<span style="color: Green;">*🖊️Convention de nommage*</span>: Les noms des contrôleurs doivent être au pluriel et refléter l'entité qu'ils gèrent, suivis de Controller.
>PersonController pour la gestion des entités Person.

# 👨‍💻 <span style="color: dodgerblue;">Application</span> 👨‍💻
##### Le cœur de l'application où la logique d'application est implémentée.

### 📁 <span style="color: deepskyblue;">InterfaceService</span>
Ces interfaces sont des contrats pour les services d'application qui orchestrent le flux de l'application sans contenir de logique métier. Ces interfaces seront implémentées par des classes de service qui contiendront la logique métier dans  `Application/Service`.

<span style="color: Green;">*🖊️Convention de nommage*</span>: Les interfaces commencent par un "I" suivi du nom du service, indiquant leur objectif.
>IPersonService pour les services relatifs aux entités Person

### 📁 <span style="color: deepskyblue;">Service</span>
Fournit l'implémentation des interfaces définies dans  `Application/Interface`, en définissant la logique métier pour interagir avec les entités `Person`.

<span style="color: Green;">*🖊️Convention de nommage*</span>: Les classes de service doivent indiquer clairement l'entité ou la fonctionnalité qu'elles gèrent.
> PersonService pour les opérations liées aux entités Person.

### 📁 <span style="color: deepskyblue;">Utilitie</span> 
Contient des outils utiles aux services, comme par exemple la génération de mots de passe sécurisés, le hachage de mots de passe, etc.


### 🗃️ <span style="color: deepskyblue;">DTO</span>
Facilite le transfert des données entre les couches de l'application.


#### ─📁 <span style="color: slateblue;">*Request*</span>
Définit les structures de données que l'API attend dans les requêtes des clients. Ils servent à capturer les données d'entrée nécessaires pour les opérations sur les différentes entités.

<span style="color: Green;">*🖊️Convention de nommage*</span>: Les noms des DTO de request se terminent par RequestDTO.
> CreatePersonRequestDTO pour a réponse DTO.

#### ─📁 <span style="color: slateblue;">*Response*</span>
Définit les structures de données que l'API renvoie en réponse aux clients. Ils structurent l'information résultant des opérations effectuées.

<span style="color: Green;">*🖊️Convention de nommage*</span>: Les noms des DTO de réponse se terminent par ResponseDTO.
> CreatePersonResponseDTO pour a réponse DTO.

# 🛠️ <span style="color:dodgerblue;">Domain</span> 🛠️
##### Contient la logique métier et les règles qui sont au cœur de l'application.

### 📁 <span style="color: deepskyblue;">Model</span>
Définit les objets du domaine métier, les entités avec lesquelles l'application travaille. Chaque fichier représente une entité différente et contient des propriétés et méthodes qui correspondent à la logique métier et aux règles de validation.

<span style="color: Green;">*🖊️Convention de nommage*</span>:  Les noms de modèle doivent correspondre directement aux entités métier qu'ils représentent, sans préfixe ou suffixe.
> Person, PersonJobTitle


### 📁 <span style="color: deepskyblue;">Utilitie</span>
Similaire au dossier `Application`, mais les outils ici sont probablement spécifiques au domaine métier.

# 💾 <span style="color:dodgerblue;">Infrastructure</span> 💾
##### Contient les détails techniques qui supportent les couches d'application et de domaine, comme la base de données, la configuration et les interfaces d'infrastructure.

### 📁 <span style="color: deepskyblue;">Configuration</span>
Comprend les fichiers utilisés pour configurer les détails des mappages entre les objets du modèle de domaine et la base de données, souvent dans le contexte d'un ORM comme Entity Framework.

<span style="color: Green;">*🖊️Convention de nommage*</span>: Les fichiers de configuration doivent indiquer l'entité et le type de configuration.
> PersonModelConfig pour la configuration ORM du modèle Person.

### 📁 <span style="color: deepskyblue;">InterfaceRepository</span>
Il contient l'interface effectuant un Contrat qui sera implémenté dans `Infrastructure/Repository`.

<span style="color: Green;">*🖊️Convention de nommage*</span>: Les interfaces commencent par un "I" suivi du nom du service, indiquant leur objectif. C'est le contrat passé avec 
`Repository`
> IPersonRepository pour l'entité person
> 
### 📁 <span style="color: deepskyblue;">Repository</span>
Implémentation concrète des interfaces définies dans le dossier `InterfaceRepository`, s'occupant de la manipulation des données des entités. Contient des implémentations spécifiques à la persistance de donnée

<span style="color: Green;">*🖊️Convention de nommage*</span>: Les fichiers sont nommées selon leurs entité plus le mot repository
> PersonRepository pour l'entité person

### 🗃️ <span style="color: deepskyblue;">Database</span>
Définit le contexte de la base de données utilisé par Entity Framework pour interagir avec la base de données, servant de pont entre les modèles et la base de données.
> Elle contient un fichier nommé AppDbContext



## <span style="color:Darkblue;"> *Voici l'arborescence d'un projet (Uniquement dossier)*  </span>

📁 API
└── 📁 Controller

📁 Application
├── 📁 DTO
│   ├── 📁 Request
│   └── 📁 Response
├── 📁 InterfaceService
├── 📁 Service
└── 📁 Utilitie

📁 Domain
├── 📁 Model
└── 📁 Utilitie

📁 Infrastructure
├── 📁 Configuration
├── 📁 Database
├── 📁 InterfaceRepository
└── 📁 Repository

Inversion de Contrôle IOC
Injection de Dépendence
## <span style="color:Darkblue;"> *Exemple d'arborescence du projet Quackers avec l'entitie Person* </span>
📁 API
├── 📁 Controller
│   └── 📄 PersonController.cs

📁 Application
├── 📁 DTO
│   ├── 📁 Request
│   │   ├── 📄 CreatePersonRequestDTO.cs
│   │   ├── 📄 DeletePersonByIdRequestDTO.cs
│   │   ├── 📄 GetAllPersonRequestDTO.cs
│   │   ├── 📄 GetPersonByIdRequestDTO.cs
│   │   ├── 📄 UpdatePasswordRequestDTO.cs
│   │   └── 📄 UpdatePhoneNumberRequestDTO.cs
│   ├── 📁 Response
│   │   ├── 📄 CreatePersonResponseDTO.cs
│   │   ├── 📄 DeletePersonByIdResponseDTO.cs
│   │   ├── 📄 GetAllPersonResponseDTO.cs
│   │   ├── 📄 GetPersonByIdResponseDTO.cs
│   │   └── 📄 UpdatePersonByIdResponseDTO.cs
│   └── 📄 PersonDTO.cs <span style="color:red;">( Utilisé afin de récupérer les infos d'un objet person de façon globale. )</span>
├── 📁 InterfaceService
│   └── 📄 IPersonService.cs
├── 📁 Service
│   └── 📄 PersonService.cs
└── 📁 Utilitie
    ├── 📄 PasswordGenerator.cs
    └── 📄 SecurityService.cs

📁 Domain
├── 📁 Model
│   ├── 📄 Person.cs 
│   ├── 📄 PersonJobTitle.cs
│   ├── 📄 PersonRole.cs
│   └── 📄 PersonStatut.cs
└── 📁 Utilitie
    ├── 📄 PasswordGenerator.cs
    └── 📄 SecurityService.cs

📁 Infrastructure
├── 📁 Configuration
│   ├── 📄 PersonJobTitleModelConfig.cs
│   ├── 📄 PersonModelConfig.cs
│   ├── 📄 PersonRoleModelConfig.cs
│   └── 📄 PersonStatutModelConfig.cs
├── 📁 Database
│   ├── 📄 AppDbContext.cs
│   └── 📁 EntityConfig
├── 📁 InterfaceRepository
│   └── 📄 IPersonRepository.cs
├── 📁 Repository
│   └── 📄 PersonRepository.cs
└── 📄 appsettings.json


