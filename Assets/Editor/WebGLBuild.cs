using System.Linq;
using UnityEditor;

public static class WebGLBuild
{
    public static void Build()
    {
        var scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        var options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = "../Bunny Bounce Web",
            target = BuildTarget.WebGL,
            options = BuildOptions.None
        };

        var report = BuildPipeline.BuildPlayer(options);
        if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            throw new System.Exception($"WebGL build failed: {report.summary.result}");
        }
    }
}
