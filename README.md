


<!-- PROJECT SHIELDS -->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/jccockre/constraints">
    <img src="ADI_Logo_AWP.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">AnalogDevices.Constraints</h3>

  <p align="center">
  Declarative specification of physical constraints in a system. Support for algebraic expressions and strongly-typed units, dimensional analysis.
    <br />
    <a href="https://github.com/jccockre/constraints"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/jccockre/constraints">View Demo</a>
    ·
    <a href="https://github.com/jccockre/constraints/issues">Report Bug</a>
    ·
    <a href="https://github.com/jccockre/constraints/issues">Request Feature</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Constraints is a C# .NET library supporting declarative specifications of constraints in a physical system. Users can define the physical parameters of the system, then express constraints in terms of algebraic expressions, modular arithmetic, and enumerated cases. Constraints are evaluated as `true` or `false`depending on whether they are satisfied by the parameter values. When a constraint resolves to `false`, a detailed message is intelligently constructed to explain the source of the violation.

Constraints obey strong typing and support dimensional analysis by way of [EngineeringUnits](https://github.com/MadsKirkFoged/EngineeringUnits). For example, the product of a `Voltage` parameter and a `Current` parameter will have type `Power` and assume units of `Watts`.

Constraint validation can be performed on-demand by invoking the `Validate()` method, or elements of the user interface can register themselves as subscribers via the `AddSubscriber()` method to receive notifications when parameter values have changed.

```
 var Voltage = new VoltageParameter("Voltage", 2, ElectricPotentialUnit.Millivolt);
 var Current = new CurrentParameter("Current", 3, ElectricCurrentUnit.Milliampere);

 // don't consume more than 1 microwatt of power
 var ConstrainPowerConsumption = Voltage.Times(Current).AtMost(Power.FromMicrowatts(1));

 // Result.Status is False
 // Result.Warning is "Voltage * Current (6e-06 W) must be less than or equal to 1E-06 W"
 var Result = ConstrainPowerConsumption.Validate();
  ```

### Built With

* [C# .NET](https://dotnet.microsoft.com/download/dotnet-framework)
* [EngineeringUnits](https://github.com/MadsKirkFoged/EngineeringUnits)



<!-- GETTING STARTED -->
## Installation

Available as Nuget package "AnalogDevices.Constraints" from Nuget.org.

  ```
  PM> Install-Package AnalogDevices.Constraints
  ```

<!-- USAGE EXAMPLES -->
## Usage

Define the parameters of a physical system, including units wherever possible.
```
 var Voltage = new VoltageParameter("Voltage", 2, ElectricPotentialUnit.Millivolt);
 var Current = new CurrentParameter("Current", 3, ElectricCurrentUnit.Milliampere);
```
Specify constraints as need-be. These can be 'hard' or 'soft' constraints - the UI determines how to interpret a violation.
```
 // don't consume more than 1 microwatt of power
 var ConstrainPowerConsumption = Voltage.Times(Current).AtMost(Power.FromMicrowatts(1));
 ```

Request validation on-demand.

```
 // Result.Status is False
 // Result.Warning is "Voltage * Current (6e-06 W) must be less than or equal to 1E-06 W"
 var Result = ConstrainPowerConsumption.Validate();
```
Propagate new input from the user and re-check the constraints.
```
 Voltage.Value = 1; // 1 millivolt
 Current.Value = 0.5; // 0.5 milliamps // Result.Status is False
 
 // Result.Status is True
 Result = ConstrainPowerConsumption.Validate();
```
Alternatively, register UI components as subscribers to the constraints to receive notifications when a value has changed.
```
 class MyFormElement : FormElement, ISubscriber
 {
    protected Constraint powerConsumption { get; }
	public MyFormElement(VoltageParameter voltage, CurrentParameter current) 
	{
	   this.powerConsumption = voltage.Times(current).AtMost(Power.FromMicrowatts(1));
	   
	   // subscribe to notifications
	   this.powerConsumption.AddSubscriber(this);
	}
	
    public void NotifyChanged()
    {
       // automatically called whenever voltage or current changes
       var result = this.powerConsumption.Validate();
       this.Enabled = result.Status;
       this.Tooltip = result.Warning;
    }
 }
```

<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/jccockre/constraints/issues) for a list of proposed features (and known issues).



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the modified MIT X11 License. See <a href="https://github.com/jccockre/constraints/license.txt">LICENSE.txt</a> for more information.



<!-- CONTACT -->
## Contact

Jason Cockrell - Jason.Cockrell@analog.com


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/jccockre/repo.svg?style=for-the-badge
[contributors-url]: https://github.com/jccockre/constraints/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/jccockre/repo.svg?style=for-the-badge
[forks-url]: https://github.com/jccockre/constraints/network/members
[stars-shield]: https://img.shields.io/github/stars/jccockre/repo.svg?style=for-the-badge
[stars-url]: https://github.com/jccockre/constraints/stargazers
[issues-shield]: https://img.shields.io/github/issues/jccockre/repo.svg?style=for-the-badge
[issues-url]: https://github.com/jccockre/constraints/issues
[license-shield]: https://img.shields.io/github/license/jccockre/repo.svg?style=for-the-badge
[license-url]: https://github.com/jccockre/constraints/blob/master/LICENSE.txt
