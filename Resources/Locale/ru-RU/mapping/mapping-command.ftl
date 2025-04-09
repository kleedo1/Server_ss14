cmd-mapping-desc = Создаёт или загружает карту и телепортирует вас на неё.
cmd-mapping-help = Использование: mapping [MapID] [Path]
cmd-mapping-server = Только игроки могут использовать эту команду.
cmd-mapping-error = При создании новой карты произошла ошибка.
cmd-mapping-try-grid = Failed to load the file as a map. Attempting to load the file as a grid...
cmd-mapping-success-load = Создаёт неинициализированную карту из файла { $path } с id { $mapId }.
cmd-mapping-success-load-grid = Loaded uninitialized grid from file { $path } onto a new map with id { $mapId }.
cmd-mapping-success = Создаёт неинициализированную карту с id { $mapId }.
cmd-mapping-warning = ПРЕДУПРЕЖДЕНИЕ: На сервере используется отладочная дебаг сборка. Вы рискуете потерять свои изменения.
# duplicate text from engine load/save map commands.
# I CBF making this PR depend on that one.
cmd-mapping-failure-integer = { $arg } это не допустимый integer.
cmd-mapping-failure-float = { $arg } это не допустимый float.
cmd-mapping-failure-bool = { $arg } это не допустимый bool.
cmd-mapping-nullspace = Вы не можете загрузиться на карту 0.
cmd-hint-mapping-id = [MapID]
cmd-mapping-hint-grid = [Grid]
cmd-hint-mapping-path = [Path]
cmd-mapping-exists = Карта { $mapId } уже существует.
