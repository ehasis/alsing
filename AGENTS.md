# AGENTS.md

This file provides guidance to AI coding agents working with code in this repository.

## Project

Alsing.SyntaxBox: a Windows Forms syntax-highlighting editor control with code folding, originally by Roger Alsing (abandoned on Google Code), migrated to GitHub and ported to modern .NET by the current maintainer. Published as the `Alsing.SyntaxBox` NuGet package (LGPL-2.1-or-later). Windows-only.

## Build

Build with Visual Studio's MSBuild from a Developer Command Prompt (or Developer PowerShell), not `dotnet build`. Visual Studio 18 is installed at `C:\Program Files\Microsoft Visual Studio\18\Enterprise`. When running from a plain shell, load the environment first. The "vswhere.exe is not recognized" message VsDevCmd prints is harmless:

```cmd
cmd /c '"C:\Program Files\Microsoft Visual Studio\18\Enterprise\Common7\Tools\VsDevCmd.bat" -no_logo && msbuild Alsing.SyntaxBox.slnx -restore -m -v:minimal'
```

Inside a Developer Command Prompt:

```cmd
msbuild Alsing.SyntaxBox.slnx -restore -m
```

```cmd
msbuild Alsing.SyntaxBox.slnx -restore -m -p:Configuration=Release
```

```cmd
msbuild src\Alsing.SyntaxBox\Alsing.SyntaxBox.csproj -restore -t:Pack -p:Configuration=Release
```

The package is written to `artifacts\package\release\`. To build a single target framework, add `-p:TargetFramework=net48` (or `net10.0-windows`). To run a sample, build it and launch the exe, for example `artifacts\bin\HelloWorld\debug_net48\HelloWorld.exe`.

- Every project multi-targets `net48;net10.0-windows`, and output folders are named `<config>_<tfm>`. Changes must compile on both; .NET Framework-only references (`System.Design`, `System.Web`) are conditioned on `TargetFrameworkIdentifier`.
- `Directory.Build.props` sets `UseArtifactsOutput`, so all build output goes to `artifacts/bin` and `artifacts/obj`, not per-project `bin/obj`.
- There is no test project and no linter. Verify changes by building and running the samples (`HelloWorld`, `MDIDemo`), including opening forms in the Visual Studio WinForms designer when touching designer-facing code.
- The library uses `AllowUnsafeBlocks` and P/Invoke-based GDI.

## Layout

- `src/Alsing.SyntaxBox` — the library. Its `README.md` is packed into the NuGet package (keep it package-appropriate; the root `README.md` is the separate repo readme).
- `samples/` — `HelloWorld` and `MDIDemo` WinForms apps referencing the library by project reference.
- `files/` — `.syn` syntax definition files (XML) for many languages; `Template.syn` is the starting point for new ones.

`RootNamespace` is `Alsing`; namespaces follow folders (`Alsing.SourceCode`, `Alsing.Windows.Forms`, `Alsing.Drawing.GDI`, ...).

## Architecture

The design is a document/view split:

**Document model (`SourceCode/`)**
- `SyntaxDocument` (a `Component`) owns the text as a `RowList` of `Row`s, the `UndoBuffer`, and an `IParser` (default `DefaultParser`). Setting `SyntaxFile` calls `Parser.Init(...)`, which loads a `SyntaxDefinition` via `SyntaxDefinitionLoader` from a `.syn` file.
- A `SyntaxDefinition` is a tree of `SpanDefinition`s (nested regions such as strings/comments/blocks, which drive both highlighting and folding) containing `PatternList`s of `Pattern`s (keywords, operators, regex/number matchers from `Text/PatternMatchers`) with `TextStyle`s.
- Parsing is incremental and per row: edits call `Parser.ParseRow(index, ...)`; each `Row` stores its parsed `Word`s and the `Span` state at its start/end, so a change only reparses until span state stabilises. `NewParser` exists but is not wired in.
- `SyntaxDocumentExporters` render a document to HTML; `SourceCodePrintDocument` handles printing.

**View (`Windows/Forms/`)**
- `SyntaxBoxControl` → `SplitViewParentControl` (`CoreLib`) → `BaseControl`. The parent hosts up to four split panes; `SplitViewParentControl.GetNewView()` is the factory, overridden by `SyntaxBoxControl` to create `EditViewControl`s. All panes share one `SyntaxDocument`. The base `GetNewView()` must return a non-null default view so the WinForms designer can construct the base class.
- `EditViewControl` (`SyntaxBox/`) is one editing pane: `Caret`, `Selection`, keyboard handling (`KeyboardAction` lists), auto-complete (`AutoListForm`), `InfoTipForm`, find/replace and goto-line dialogs.
- Rendering goes through `IPainter`, implemented by `NativePainter`, which draws with the GDI wrappers in `Drawing/GDI` (`GDISurface`, `GDIFont`, `GDIBrush`, ...) rather than System.Drawing for performance.
- `Design/` holds design-time support (collection editors, type converters); `SyntaxBoxDesigner` is the control designer.

## Conventions

- Forms/controls use the standard WinForms `X.cs` + `X.Designer.cs` + `X.resx` split; keep `InitializeComponent` in the `.Designer.cs` file in the format the VS designer emits.
- Public settable properties on controls/forms that should not be serialized by the designer get `[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]` (required by the .NET WinForms designer analyzers).
