# Проверка квеста по файловой системе
* Проверяющий: Каралюс Никита
* Проверяемый: Ябров Илья

## Описание
Структурно квест состоит из множества папок и загадки в корневой директории. Правильному ответу соответсвует номер папки. Папки с неправильным ответом пустые, правильные вновь содержат другое множество папок вместе со следующей загадкой. Из-за этого получается различие в глубине, поэтому квест легко взламывается командой
`ls ./*/*/*/*` (Unix) 🤓

### Задание 1
**Системы компьютерной математики.** Необходимо посчитать предел от выржаения. Я использовал Wolfram Alpha. **Ответ: 97.**

### Задание 2
**Измерение Информации.** Для решения нужно использовать формулу Шеннона.
**Ответ: 10.**

### Задание 3
**Решение оптимизационных задач.** Необходимо вспомнить, как назывался инструмент, который мы использовали в Excel, посчитать количество букв в его названии без пробелом и перейти в соотвествующую папку.

*Оказывается, нужно было вычесть из общего количество папок количество букв. В тексте задания это понять проблематично.*

**Ответ: 7765**

## Оценка

Квест приятный, с картинками, с шутками. Илья использовал разные пройденные темы для составления. Квест несложный, проходится в пределах 7 минут. Награда тоже оригинальная. Я снижу 5 баллов из-за непонятной формулировки последнего задания.

**Результат: 95/100.**