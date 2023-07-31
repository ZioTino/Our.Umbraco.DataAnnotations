﻿using System.Diagnostics;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Manifest;

namespace Our.Umbraco.ValidationAttributes.UI;

public class StaticAssetsBoot : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AdduSyncStaticAssets();
    }
}

public static class uSyncStaticAssetsExtensions
{
    public static IUmbracoBuilder AdduSyncStaticAssets(this IUmbracoBuilder builder)
    {
        // don't add if the filter is already there .
        if (builder.ManifestFilters().Has<uSyncAssetManifestFilter>())
            return builder;

        // add the package manifest programatically. 
        builder.ManifestFilters().Append<uSyncAssetManifestFilter>();

        return builder;
    }
}

internal class uSyncAssetManifestFilter : IManifestFilter
{
    public void Filter(List<PackageManifest> manifests)
    {
        var assembly = typeof(uSyncAssetManifestFilter).Assembly;
        FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
        string version = fileVersionInfo.ProductVersion;
            
        manifests.Add(new PackageManifest
        {
            PackageName = "OurUmbracoValidationAttributes",
            
            BundleOptions = BundleOptions.None,
            Scripts = new string[]
            {
            },
            Stylesheets = new string[]
            {
            }
        }); ;
    }
}