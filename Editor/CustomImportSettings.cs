using UnityEditor;
public class CustomImportSettings : AssetPostprocessor
{
    private void OnPreprocessModel()
    {
        ModelImporter importer;
        importer = assetImporter as ModelImporter;
        importer.materialImportMode = ModelImporterMaterialImportMode.None;
    }

    private void OnPostprocessTexture()
    {
        TextureImporter importer;

        importer = assetImporter as TextureImporter;
        importer.mipmapEnabled = false;
    }
}
