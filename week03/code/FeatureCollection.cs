using System.Text.Json;

public class FeatureCollection
{
    // TODO Problem 5 - ADD YOUR CODE HERE
    // Create additional classes as necessary

    public FeaturesClass[] Features { get; set; }
}
public class FeaturesClass
{
    public PropertiesClass Properties { get; set; }
}
public class PropertiesClass
{
    public string Place { get; set; }
    public float Mag { get; set; }
}