#OpenCodeDev.NetCMS
Open-Source .NET CMS Inspired of Hapi and Wordpress on many aspect and Strapi on Admin Side.

Goal is to offer and easy way to create api tables, execute CRUD and set permission.

Not only that but also allow deploy as cluster with central admin management so can be scaled lastly allow to sperate the admin dashboard from the api server.




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
