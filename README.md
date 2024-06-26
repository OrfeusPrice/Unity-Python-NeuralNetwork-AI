# Unity-Python-NeuralNetwork-AI
Для проекта были использованы:

•	VisualStudio Code и Python версии 3.9.13

•	VisualStudio 2022 и C# 6.0

•	Unity3D – Editor version: 2022.3.28f1

ВАЖНО: Для работы с Python следует открывать всю папку Assets как рабочее пространство в VS Code, а не отдельно скрипт NNPy.py.  



Как управлять проектом:

• Запустить в среде Unity.

• Далее действия на выбор пользователя:

Создать ИИ со случайными данными и обработать их:

1.	E – Создать 1000 Random AI (по желанию можно нажать клавишу E несколько раз и создать больше AI, главное, чтобы в train.csv и test.csv их количество совпадало)

2.	Ждём, пока все AI пропадут со сцены в Unity (пропадут в случае достижения финишной черты, или если пройдут по всему маршруту из 10 координатных точек и не дойдут до финиша)

3.	F – Создаём файл train.csv

4.	R – Удаляем всех ИИ из списка, чтобы они не участвовали в заполнении будущих данных

5.	E – Снова создаём Random AI (Как в П.1)

6.	Ждём, пока AI отработают (Как в П.2)

7.	 G – Создаём файл posx.csv

Замечание: Клавишу G можно нажать сразу же после П.5.  

8.	T – Создаём файл test.csv

9.	Переходим в скрипт Python и запускаем его

Замечание: Реализовать работу со скриптом на языке Python из Unity не получилось, потому что Python – интерпретируемый ЯП и не имеет исполнительного файла.

10.	 Получаем файл pers.csv. Весь цикл можно повторить после нажатия клавиши R. 



Создать обученных ИИ:

1.	С – получить данные о координатах и их весах.

2.	V – Создать 1000 Fit AI (по желанию можно нажать клавишу V несколько раз и создать больше AI, главное, чтобы в train.csv и test.csv их количество совпадало)

3.	Ждём, пока все AI пропадут со сцены в Unity (пропадут в случае достижения финишной черты, или если пройдут по всему маршруту из 10 координатных точек и не дойдут до финиша)

4.	A – Создаём файл train.csv

5.	X – Удаляем всех ИИ из списка, чтобы они не участвовали в заполнении будущих данных

6.	V – Снова создаём Fit AI (Как в П.2)

7.	Ждём, пока AI отработают (Как в П.3)

8.	D – Создаём файл posx.csv

Замечание: Клавишу G можно нажать сразу же после П.6.  

9.	S – Создаём файл test.csv

10.	Переходим в скрипт Python и запускаем его

Замечание: Реализовать работу со скриптом на языке Python из Unity не получилось, потому что Python – интерпретируемый ЯП и не имеет исполнительного файла.

11.	Получаем файл pers.csv. Весь цикл можно повторить после нажатия клавиши X. 



Если Fit AI были обучены хотя бы один раз, будет иметь смысл запустить сцену в Unity и нажать клавиши в таком порядке: C, одновременно E и V. Это создаст одновременно ИИ со случайными данными и обученных ИИ. При визуальном отслеживании их поведения можно заметить, что обученные ИИ в основной своей массе исчезают гораздо раньше необученных, а так же движутся до финиша по гораздо более прямой траектории. С каждым поколением разница будет всё более заметна. 

