pocketeer [![Status Enfer][status-enfer]][andivionian-status-classifier]
=========

pocketeer is a tool for managing [Pocket][pocket]. It allows to fetch the data
from Pocket to keep and analyze it locally.

Build
-----

```console
$ msbuild /p:Configuration=Release
```

Use
---

First of all, you'll need pocket API consumer key. Generate one at [Pocket
developer site][pocket-developer].

After that, generate API access token. **TODO**: Application will help with that
in future. You can retrieve a token from configuration of installed [pocket-cli]
at the moment.

After that, execute in console:

```console
$ pocketeer <consumer key> <API token>
```

That'll fetch your pocket data.

TODO: In future, the data will be stored in JSON file for future analysis.

[andivionian-status-classifier]: https://github.com/ForNeVeR/andivionian-status-classifier##status-enfer-
[pocket]: https://getpocket.com/
[pocket-cli]: https://github.com/rakanalh/pocket-cli
[pocket-developer]: https://getpocket.com/developer/apps/new

[status-enfer]: https://img.shields.io/badge/status-enfer-orange.svg
