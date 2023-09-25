// https://github.com/fsprojects/FAKE/issues/2719
#r "nuget: Fake.Core.Target"

open Fake.Core
open System.IO

// Boilerplate
System.Environment.GetCommandLineArgs()
|> Array.skip 2 // skip fsi.exe; build.fsx
|> Array.toList
|> Context.FakeExecutionContext.Create false __SOURCE_FILE__
|> Context.RuntimeContext.Fake
|> Context.setExecutionContext

let PROJECT_NAME = "postgres_playground"
let ENV = ".env"
let DATA_DIR = "./database/data"
let DOCKER_COMPOSE_LOCAL_DATABASE = "database/docker-compose.yaml"


Target.create "up" (fun _ ->
    Shell.Exec(
        "docker",
        [ $"compose -p {PROJECT_NAME}"
          $"-f {DOCKER_COMPOSE_LOCAL_DATABASE}"
          $"--env-file {ENV}"
          "up -d" ]
        |> String.concat " "
    )
    |> ignore)

Target.create "clean" (fun _ ->
    if Directory.Exists DATA_DIR then
        Directory.Delete(DATA_DIR, true))

Target.create "down" (fun _ ->
    Shell.Exec(
        "docker",
        [ $"compose -p {PROJECT_NAME}"; "down --rmi all --volumes --remove-orphans" ]
        |> String.concat " "
    )
    |> ignore)

open Fake.Core.TargetOperators

"down" ==> "clean" ==> "up"

Target.runOrDefaultWithArguments "up"
