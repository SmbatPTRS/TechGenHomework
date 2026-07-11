# CopyTool

A small command-line file copy utility written in C#. Copies a file in chunks, showing live progress and an estimated time remaining (ETA).

## What it does

- Copies a file from a source path to a destination path.
- Reads/writes in configurable-size chunks instead of loading the whole file into memory.
- Shows a live-updating progress percentage and ETA while copying.
- Creates the destination folder automatically if it doesn't exist.

## Usage

```
--source <path> --dest <path> --buffer <size>
```

### Arguments

| Argument | Required | Description |
|---|---|---|
| `--source` | Yes | Path to the file you want to copy. Must already exist. |
| `--dest` | Yes | Path to write the copy to. Created if it doesn't exist; overwritten if it does. |
| `--buffer` | No | Chunk size in bytes used while copying. Defaults to `4096` if omitted or invalid. |

### Example

```
--source "C:\Users\Desktop\sourcefile.txt" --dest "C:\Users\Desktop\destination.txt" --buffer 4096
```

## Notes

- Paths can be relative (to the program's working directory) or absolute — absolute is safer while testing, since the working directory isn't always the project folder.
- If `--source` doesn't exist, the program prints an error and exits.
- If `--dest`'s parent folder doesn't exist, it's created automatically.
- Buffer size affects copy speed/memory usage per chunk, not correctness — 4096 (4 KB) is a reasonable default for most files.
