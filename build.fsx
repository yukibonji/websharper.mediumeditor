#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.MediumEditor")
        .VersionFrom("WebSharper", versionSpec = "(,4.0)")
        .WithFSharpVersion(FSharpVersion.FSharp30)
        .WithFramework(fun f -> f.Net40)

let main =
    bt.WebSharper.Extension("WebSharper.MediumEditor")
        .SourcesFromProject()
        .Embed([])
        .References(fun r -> [])

let tests =
    bt.WebSharper.SiteletWebsite("WebSharper.MediumEditor.Tests")
        .SourcesFromProject()
        .Embed([])
        .References(fun r ->
            [
                r.Project(main)
                r.NuGet("WebSharper.Testing").Version("(,4.0)").Reference()
                r.NuGet("WebSharper.UI.Next").Version("(,4.0)").Reference()
            ])

bt.Solution [
    main
    tests

    bt.NuGet.CreatePackage()
        .Configure(fun c ->
            { c with
                Title = Some "WebSharper bindings for MediumEditor"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://github.com/intellifactory/https://github.com/intellifactory/websharper.mediumeditor"
                Description = "WebSharper Extension for Medium Editor 5.23.1"
                RequiresLicenseAcceptance = true })
        .Add(main)
]
|> bt.Dispatch
