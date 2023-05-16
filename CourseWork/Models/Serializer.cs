using Avalonia;
using Avalonia.Input;
using CourseWork.Models.LogicalElements;
using CourseWork.Models.SerializebleElements;
using DynamicData;
using Newtonsoft.Json;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using YamlDotNet.Serialization;
using Formatting = Newtonsoft.Json.Formatting;
using InputElement = CourseWork.Models.LogicalElements.InputElement;

namespace CourseWork.Models
{
    public static class Serializer
    {
        public static void Save(string path, Project data)
        {
            Project project = ToSerializeble(data);
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };
            string Serialized = JsonConvert.SerializeObject(project, settings);
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.WriteLine(Serialized);
            }
        }

        public static Project Load(string path)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };
            string Serialized;
            using (StreamReader reader = new StreamReader(path))
            {
                Serialized = reader.ReadToEnd();
            }
            return FromSerializeble(JsonConvert.DeserializeObject<Project>(Serialized, settings));
        }

        public static Project ToSerializeble(Project data)
        {
            Project new_project = new Project { Name = data.Name};
            var schemes = data.Schemes;
            ObservableCollection<Scheme> new_schemes = new ObservableCollection<Scheme>();
            foreach (Scheme scheme in schemes)
            {
                ObservableCollection<Element> new_elements = new ObservableCollection<Element>();
                var elements = scheme.Elements;
                foreach(Element element in elements)
                {
                    if (element is Connector connector)
                    {
                        new_elements.Add(new SerializebleConnector
                        {
                            ID = connector.ID,
                            FocusOnElement = connector.FocusOnElement,
                            ConnectFromFirstInput = connector.ConnectFromFirstInput,
                            ConnectToFirstInput = connector.ConnectToFirstInput,
                            ReverseConnection = connector.ReverseConnection,
                            StartPoint = connector.StartPoint.ToString(),
                            EndPoint = connector.EndPoint.ToString(),
                            FirstElement = connector.FirstElement.ID,
                            SecondElement = connector.SecondElement.ID
                        });
                        continue;
                    }
                    if (element is InputElement input)
                    {
                        new_elements.Add(new SerializebleInput { ID = input.ID, FocusOnElement = input.FocusOnElement, SignalOut = input.SignalOut, StartPoint = input.StartPoint.ToString() });
                        continue;
                    }
                    if (element is OutputElement output)
                    {
                        new_elements.Add(new SerializebleOutput { ID = output.ID, FocusOnElement = output.FocusOnElement, IsConnected = output.IsConnected, SignalIn = output.SignalIn, StartPoint = output.StartPoint.ToString() });
                        continue;
                    }
                    if (element is TwoInputsGate two)
                    {
                        var elem = new SerializebleTwoInputElements { ID = two.ID, FocusOnElement = two.FocusOnElement, Input1 = two.Input1, Input2 = two.Input2, IsConnectedInput1 = two.IsConnectedInput1, IsConnectedInput2 = two.IsConnectedInput2, StartPoint = two.StartPoint.ToString() };
                        if (two is AndGate) elem.Type = "AndGate";
                        if (two is OrGate) elem.Type = "OrGate";
                        if (two is XorGate) elem.Type = "XorGate";
                        if (two is SemiSummatorGate) elem.Type = "SemiSummatorGate";
                        new_elements.Add(elem);
                        continue;
                    }
                    
                    if (element is NotGate not)
                    {
                        new_elements.Add(new SerializebleNot { ID = not.ID, FocusOnElement = not.FocusOnElement, Input = not.Input, IsConnected = not.IsConnected, StartPoint = not.StartPoint.ToString() });
                        continue;
                    }
                }
                new_schemes.Add(new Scheme { Name = scheme.Name, Elements = new_elements });
            }
            new_project.Schemes = new_schemes;
            return new_project;
        }

        public static Project FromSerializeble(Project data)
        {
            Project new_project = new Project { Name = data.Name };
            var schemes = data.Schemes;
            schemes.RemoveAt(0);
            ObservableCollection<Scheme> new_schemes = new ObservableCollection<Scheme>();
            foreach (Scheme scheme in schemes)
            {
                ObservableCollection<Element> new_elements = new ObservableCollection<Element>();
                var elements = scheme.Elements;
                foreach (Element element in elements)
                {
                    if (element is SerializebleConnector connector)
                    {
                        Connector new_connector = new Connector
                        {
                            ID = connector.ID,
                            FocusOnElement = connector.FocusOnElement,
                            ConnectFromFirstInput = connector.ConnectFromFirstInput,
                            ConnectToFirstInput = connector.ConnectToFirstInput,
                            ReverseConnection = connector.ReverseConnection,
                            StartPoint = Point.Parse(connector.StartPoint),
                            EndPoint = Point.Parse(connector.EndPoint)
                        };
                        foreach(Element el in new_elements)
                        {
                            if (el is LogicalElement log)
                            {
                                if (log.ID == connector.FirstElement) { new_connector.FirstElement = log; break; }
                            }
                        }
                        foreach (Element el in new_elements)
                        {
                            if (el is LogicalElement log)
                            {
                                if (log.ID == connector.SecondElement) { new_connector.SecondElement = log; break; }
                            }
                        }
                        new_elements.Add(new_connector);
                        continue;
                    }
                    if (element is SerializebleInput input)
                    {
                        new_elements.Add(new InputElement { ID = input.ID, FocusOnElement = input.FocusOnElement, SignalOut = input.SignalOut, StartPoint = Point.Parse(input.StartPoint) });
                        continue;
                    }
                    if (element is SerializebleOutput output)
                    {
                        new_elements.Add(new OutputElement { ID = output.ID, FocusOnElement = output.FocusOnElement, IsConnected = output.IsConnected, SignalIn = output.SignalIn, StartPoint = Point.Parse(output.StartPoint) });
                        continue;
                    }
                    if (element is SerializebleNot not)
                    {
                        new_elements.Add(new NotGate { ID = not.ID, FocusOnElement = not.FocusOnElement, Input = not.Input, StartPoint = Point.Parse(not.StartPoint), IsConnected = not.IsConnected });
                        continue;
                    }
                    if (element is SerializebleTwoInputElements two)
                    {
                        if (two.Type == "AndGate")
                        {
                            new_elements.Add(new AndGate { ID = two.ID, FocusOnElement = two.FocusOnElement, Input1 = two.Input1, Input2 = two.Input2, IsConnectedInput1 = two.IsConnectedInput1, IsConnectedInput2 = two.IsConnectedInput2, StartPoint = Point.Parse(two.StartPoint) });
                            continue;
                        }
                        if (two.Type == "OrGate")
                        {
                            new_elements.Add(new OrGate { ID = two.ID, FocusOnElement = two.FocusOnElement, Input1 = two.Input1, Input2 = two.Input2, IsConnectedInput1 = two.IsConnectedInput1, IsConnectedInput2 = two.IsConnectedInput2, StartPoint = Point.Parse(two.StartPoint) });
                            continue;
                        }
                        if (two.Type == "XorGate")
                        {
                            new_elements.Add(new XorGate { ID = two.ID, FocusOnElement = two.FocusOnElement, Input1 = two.Input1, Input2 = two.Input2, IsConnectedInput1 = two.IsConnectedInput1, IsConnectedInput2 = two.IsConnectedInput2, StartPoint = Point.Parse(two.StartPoint) });
                            continue;
                        }
                        if (two.Type == "SemiSummatorGate")
                        {
                            new_elements.Add(new SemiSummatorGate { ID = two.ID, FocusOnElement = two.FocusOnElement, Input1 = two.Input1, Input2 = two.Input2, IsConnectedInput1 = two.IsConnectedInput1, IsConnectedInput2 = two.IsConnectedInput2, StartPoint = Point.Parse(two.StartPoint) });
                            continue;
                        }
                    }
                }
                new_schemes.Add(new Scheme { Name = scheme.Name, Elements = new_elements });
            }
            new_project.Schemes = new_schemes;
            return new_project;
        }


    }

}
