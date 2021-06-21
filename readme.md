#OpenCodeDev.NetCMS
Open-Source .NET CMS Inspired of Hapi and Wordpress on many aspect and Strapi on Admin Side.

Goal is to offer and easy way to create api tables, execute CRUD and set permission.

Not only that but also allow deploy as cluster with central admin management so can be scaled lastly allow to sperate the admin dashboard from the api server.

Project still very early at idea stage we"ll build up from there using well master concepts.

## Why?
I never found exactly a good tool that is consistent especially when it come to data.

Example: Strapi sometime return {"key":null}, {"key":0} or {"key":{"id":0}}... data integrity is not there also if you build micro-services it can be complex to handle permissions and auth when contacting a specific service... especially when it comes to auth our goal here is to avoid having to contact other server for permission and auth purposes making request faster.

Because we want to leverage GRPC power but also allow classic JSON API wrapped around GRPC without having to create twice the behavior.

Lastly, while using strapi or wordpress it is great to have the administration built in but it can be a horrible thing when you have to handle multiple services... that why we aim to create the administration the same way strapi does with cutting edge Blazor tech-stack but can be easly hosted elsewhere like a seperated application but also can handle multiple related micro-service and integrate their data from a single dashboard.

# Rough Goals
1. Avoid Using Reflection at Runtime (During Request), as much as possible.
2. Use Reflection at Compile Time, Make File Changes Before Compile.
3. Load Plugin at Runtime (Load Before server available) with Reflection.
4. Try to make a system that will avoid Hook thru reflection (maybe squash everything in during compiling?)
# Possible Roadmap
## 2021
- Can Create GRPC/JSON Api routes and call its CRUD from GRPC and JSON POST/GET/PUT/DEL
- Can Request 1 Depth Nested Relation to be return.
- Can Fetch with Extensive Smart Search System.
- Can Define and Set Public/Stric Private Model Fields.
- Can Extend Administration Dashboard.
- Support Plugin at Runtime.
- Can Develop Back-End Plugins with Front-End (Dashboard).
- Auto-Generate Basic Api Behavior (CRUD) with CLI.

## 2022
- Completed Open-Source Client-Side.
- Support for Deepnesting Level (Circular will be nulled).
- Auto-Generated Basic Api Behavior (CRUD from Dashboard).
- Fully Support Plugin at Runtime.
- Thats all i could think of for the moment.

# NOTE TO MYSELF
Config -> Json Routes (Grpc or Traditional Api) Grpc:true, WebApi: true, DataContext: "Name" (Relation must be same DbContext)
Controllers -> Function are Called from (ControllerBase) by default.
Models -> Model Information (Table) Fields and Relations initially from (Model[Name]Base)
Services -> Set of Extra Functions Accessible from any api


App -> Create Api Route -> Construct
netcms generate:api -grpc -json "Recipes" -> Create Folder and Files (Json Config, C#)
netcms reconstruct -> Rebuild C# Classes of Model.
netcms remodel -> Update Message Request Fields
netcms refresh-legacy -> Check any change and Regenerate Wrapper for Grpc to JSON API
netcms prebuild "Solution Path"

netcms generate:api "Recipe"
	- Create Folder Named "Recipe" in "Server/Api"
	- Create Sub-Folders "Controllers", "Models", "Services"
	- Create Folder Named "Recipe" in "Shared/Api"
	- Create Sub-Folders "Controllers", "Models", "Services"

	- Create File "RecipeController.cs" in "Server/Api/Recipe/Controllers"
	- Create File "RecipeControllerLegacy.cs" in "Server/Api/Recipe/Controllers"
	- Create File "RecipePrivateModel.cs" in "Server/Api/Recipe/Models"
	- Create File "Recipeservice.cs" in "Server/Api/Recipe/Service"

	- Create File "IRecipeController.cs" in "Shared/Api/Controllers"
	- Create File "RecipeFetchRequest.cs" in "Shared/Api/Messages"
	- Create File "RecipePublicModel.cs" in "Shared/Api/Models"


Permission -> Handle Permission Logic and Behavior (Shared Across All Plugins)
  -> Load and List All OperationContract (Need to Test how to Detect Overrides and Hiarchi)
  -> Generates JWT Token, Validate and Decrypt Token
  -> Generates Api Keys, Validate and Decrypt
  -> As Master, When notified by account server that a role has changed on user, permission register user ID, Date of Change and Expiry.
  -> As Master, Whenever JWT is add or remove other slave-clusters are notified.
  -> As Slave, whenever i boot up, i notify the master and wait for greenlight.
  -> As Master, when slave notifies, master sends all active user changes, master lastly send green light signal. (Server up and runing)
  -> As Permission Plugin, when validating JWT's role, check if the requesting user's JWt creation date preceed the change date if so, send error to disconnect the user so it can get a new token. else allow acces since token date is later than last role change

  -> In-Memory Database 
  -> Which Role can call which Controller ? (CRUD)
  -> Which Role can See/Change which field (private = never show anyone like pwd) (restricted = specific role only) on request strip 

Permission.Account -> Handle Basic User Info Login (Register/Login/2FA/Reset PWD/Email Confirm)

Permission.ApiKey -> Handle ApiKeys (Create, Update, Delete)
