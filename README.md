# Postgres Local DB Minimal Sample

Postgresのローカル開発環境を一発で構築するためのサンプルです。

## Setup
set `.env` file (see `.env.example`)

```sh
./fake.sh up
# or
sh fake.sh up
``` 

## Clean up
```sh
./fake.sh down
# or
sh fake.sh down
``` 

## Fake - F# Make
[FAKE](https://fake.build/)とは、F#によりビルドタスクを型安全・宣言的に定義するツールです。

使い方に関しては[公式ドキュメント](https://fake.build/guide/getting-started.html)が参考になります。ただし、https://github.com/fsprojects/FAKE/issues/2719 に報告される問題を回避するため、`.NET 7`以上のバージョンを使用する際は、以下のように`build.fsx`を記述してください。

```fsharp
#r "nuget: Fake.Core.Target"

open Fake.Core

// Boilerplate
System.Environment.GetCommandLineArgs()
|> Array.skip 2 // skip fsi.exe; build.fsx
|> Array.toList
|> Context.FakeExecutionContext.Create false __SOURCE_FILE__
|> Context.RuntimeContext.Fake
|> Context.setExecutionContext

// ...
```
