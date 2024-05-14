# Ozds.Client

<div style="display: none;">
  \page structure-client Client
</div>

This project holds all the UI pages and reusable components needed to render the
UI. Pages are kept in the `Pages` namespace and reusable components are kept in
the `Shared` namespace. Pages in the `Pages` namespace are kept at minimum
complexity, contain only one route, and most UI logic is kept in the reusable
components. Reusable components only contain UI logic and any backend logic is
kept in the `Ozds.Business` project.

## Ozds.Client.Base

Contains base component and layout component classes for the UI. These classes
provide functions that make it more convenient to serialize dates, measures and
localize text.

```plantuml
abstract class OzdsComponentBase
{
  # string DecimalString(decimal number, int? places = null)
  # string FloatString(float number, int? places = null)
  # string DateString(DateTimeOffset? date)
  # string DateTimeString(DateTimeOffset? date)
}
```

## Ozds.Client.Pages

Contains pages rendered by the UI. Pages are better explained in other parts of
the documentation.

## Ozds.Client.Shared

Contains reusable components. Reusable components are spread across multiple
namespaces each for a different purpose:

- Models: contains components directly tied to individual models in the
  `Ozds.Business` project. These are components used to view and potentially
  edit and audit models.

- Print: contains components used to render pdf documents. These only contain
  components used to render invoices for now.

- Layout: contains components used to render the layout of the UI. These
  components are used to render the header, footer, and other common parts of
  the UI.
