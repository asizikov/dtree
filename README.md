# dtree

`dtree` is a dotnet clone of `tree` cli utitliy.

## Usage

```bash
$ dtree
```

sample output:

```bash
📁 .
├── 📁 .devcontainer
│   └── devcontainer.json
├── 📁 src
│   ├── 📁 bin
│   │   └── 📁 Debug
│   │      └── 📁 net7.0
│   │         ├── dtree
│   │         ├── dtree.deps.json
│   │         ├── dtree.dll
│   │         ├── dtree.pdb
│   │         └── dtree.runtimeconfig.json
│   ├── 📁 obj
│   │   ├── 📁 Debug
│   │   │   └── 📁 net7.0
│   │   │      ├── 📁 ref
│   │   │      │   └── dtree.dll
│   │   │      ├── 📁 refint
│   │   │      │   └── dtree.dll
│   │   │      ├── .NETCoreApp,Version=v7.0.AssemblyAttributes.cs
│   │   │      ├── apphost
│   │   │      ├── dtree.AssemblyInfo.cs
│   │   │      ├── dtree.AssemblyInfoInputs.cache
│   │   │      ├── dtree.assets.cache
│   │   │      ├── dtree.csproj.AssemblyReference.cache
│   │   │      ├── dtree.csproj.CoreCompileInputs.cache
│   │   │      ├── dtree.csproj.FileListAbsolute.txt
│   │   │      ├── dtree.dll
│   │   │      ├── dtree.GeneratedMSBuildEditorConfig.editorconfig
│   │   │      ├── dtree.genruntimeconfig.cache
│   │   │      ├── dtree.GlobalUsings.g.cs
│   │   │      └── dtree.pdb
│   │   ├── dtree.csproj.nuget.dgspec.json
│   │   ├── dtree.csproj.nuget.g.props
│   │   ├── dtree.csproj.nuget.g.targets
│   │   ├── project.assets.json
│   │   └── project.nuget.cache
│   ├── dtree.csproj
│   ├── Program.cs
│   └── TreeNode.cs
├── .gitignore
└── README.md
```

## Installation

```bash
$ dotnet tool install -g dtree
```

## Uninstallation

```bash
$ dotnet tool uninstall -g dtree
```
