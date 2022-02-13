from os import listdir, walk
from os.path import isfile, join
from re import match, sub
from io import StringIO
from shutil import copy
import yaml

html = '''
<!DOCTYPE html>
<html>
<head>
  <link rel="stylesheet" href="src\\style.css">
</head>
<body>
    <h1>Названия кораблей</h1>
    <table>
'''

f = open("ReadMe.html", 'w', encoding="utf-8")

localPath = r"..\..\Assets\Ships\Enemies"

subFolders: list[str] = [f for f in listdir(localPath) if not isfile(join(localPath, f))]
path = [walk(localPath, True)]

for folder in subFolders:
    tmp: list[str] = listdir(join(localPath, folder))
    pngFile: str = list(filter(
        lambda x: isfile(join(localPath, folder, x)) and match(r".+[.]png$", x) and x.split('.')[0] == folder, tmp))[0]
    prefabFile = list(filter(
        lambda x: isfile(join(localPath, folder, x)) and match(r".+[.]prefab$", x) and x.split('.')[0] == folder, tmp))[
        0]

    copy(join(localPath, folder, pngFile), join("src", pngFile))

    prefabYaml = str()
    with open(join(localPath, folder, prefabFile), 'r') as file:
        for line in file.readlines():
            prefabYaml += '--- ' + line.split(' ')[2] + '\n' if line.startswith('--- !u!') else line

    prefab = {}
    for data in yaml.load_all(prefabYaml):
        prefab |= data
        if "MonoBehaviour" in data.keys():
            break

    html += f'''
            <tr>
              <td>
                <h3>{prefab['GameObject']['m_Name']}</h3>
                <h6>Shield:{prefab['MonoBehaviour']['maxShield']}</h6>
                <h6>Hp:{prefab['MonoBehaviour']['maxHealth']}</h6>
              </td>
              <td>
                <img height=120 src="src\\{pngFile}">
              </td>
            </tr>
    '''

html += '''
    </table>
  <div>
    <h1>Создание уровней</h1>
    Сценарий уровня должен лежать в <code>D:\level.txt</code>
    <br>
    <br>
    <span>Описание команд:</span>

    <div class="command-desc">
      <code>spawn(var_name,ship_type,x,y)</code> - создание нового объекта<br>
      <br>
      <code>var_name</code> - имя новой переменной<br>
      <code>ship_type</code> - тип корабля<br>
      <code>x</code> - позиция по Х<br>
      <code>y</code> - позиция по Y<br>
    </div>

    <div class="command-desc">
      <code>move(var_name,x,y)</code> - задать скорость движение объекта<br>
      <br>
      <code>var_name</code> - имя переменной<br>
      <code>x</code> - длина вектора по оси Х<br>
      <code>y</code> - длина вектора по оси Y<br>
    </div>

    <div class="command-desc">
      <code>localMove(var_name,x,y)</code> - задать скорость движение объекта с учётом текущего поврота объекта<br>
      <br>
      <code>var_name</code> - имя переменной<br>
      <code>x</code> - длина вектора по оси Х<br>
      <code>y</code> - длина вектора по оси Y<br>
    </div>

    <div class="command-desc">
      <code>stop(var_name)</code> - остановить движение объекта<br>
      <br>
      <code>var_name</code> - имя переменной<br>
    </div>

    <div class="command-desc">
      <code>shot(var_name)</code> - объект делает выстрел<br>
      <br>
      <code>var_name</code> - имя переменной<br>
    </div>

    <div class="command-desc">
      <code>wait(time)</code> - скрипт простаевает(ни одно свойство объект не меняеться)<br>
      <br>
      <code>time</code> - время простоя в секундах<br>
    </div>

    <div class="command-desc">
      <code>rotation(var_name,speed)</code> - задать скорость вращения объекта в гр/сек<br>
      <br>
      <code>var_name</code> - имя переменной<br>
      <code>speed</code> - скорость вращения<br>
    </div>

    <div class="command-desc">
      <code>rotate(var_name,n)</code> - задать объекту угол<br>
      <br>
      <code>var_name</code> - имя переменной<br>
      <code>n</code> - угол поврота<br>
    </div>

    <div class="command-desc">
      <code>stopRotation(var_name)</code> - остановить вращение объекта<br>
      <br>
      <code>var_name</code> - имя переменной<br>
    </div>

  </div>
</body>
'''

f.write(html)
f.close()
