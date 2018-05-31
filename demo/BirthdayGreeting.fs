namespace TalkToYourFunctions

open Microsoft.Azure.WebJobs
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open Microsoft.Azure.WebJobs.Host
open DialogFlow
open Newtonsoft.Json
open System.IO
open Util

module BirthdayGreeting =
        
    [<FunctionName("BirthdayGreeting")>]
    let run([<HttpTrigger(Extensions.Http.AuthorizationLevel.Anonymous, "post", Route = null)>]req: HttpRequest, log: TraceWriter) =
        log.Info("BirthdayGreeting is processing a request.")
        async {
            try
                use reader = new StreamReader(req.Body)
                let! requestBody = reader.ReadToEndAsync() |> Async.AwaitTask
                log.Info(sprintf "Raw Request Body: %s" requestBody)

                let data = JsonConvert.DeserializeObject<DialogFlowRequest>(requestBody)
                log.Info(sprintf "Deserialized Request Body: %A" data)

                let givenName = data.queryResult.parameters.``given-name``
                let birthdate = data.queryResult.parameters.birthdate.Date

                let greeting = getGreeting givenName birthdate
                log.Info(sprintf "Responding with greeting: %s" greeting)

                let result = 
                    { payload = 
                        { google = 
                            { richResponse = 
                                { items = 
                                    [{ simpleResponse = 
                                        { textToSpeech = greeting }}]}}}}
                
                return JsonResult result :> IActionResult

            with ex -> 
                log.Error(sprintf "Something went horribly wrong!", ex)
                return BadRequestObjectResult ex.Message :> IActionResult

        } |> Async.RunSynchronously