# Terminal Game

One simple game for running in command line interface.

## Idea

### File Directory Map

with directory as map.

#### Example

```sh
- MainWorld
  |- Land
    |- Snow
      |- Wolf.entity
      |- Rabbit.entity
      |- Town
        |- ...
    |- Forest
      |- ElfVilage
        |- Elf.entity
        |- and elf chat.option
        |- attack elf.option
        |- buy item.option
        |- sell item.option
      |- Apple.prop
    |- Plain
      |- Human.entity
      |- Town
        |- ...
  |- Sea
    |- Island
    |- Ship
```

### File Entity

With file as entity.

Select file can implement opeate.

#### Example

```yaml
# Elf.entity
name: 'Elf'
sex: 1
level: 1
# ...
```

```shell
# and elf chat.option
echo "you start and elf chat."
sleep 1
echo "you start say joke."
sleep 1
echo "the chat end and you mood has improved."
```

### Todo

- watch file directory change and handle change.

- game system.

  - collect materials then process it's.

  - and npc interactive.
