import tensorflow as tf
import sklearn as sk
from sklearn.preprocessing import LabelEncoder
from keras.models import Sequential
from keras.layers import Dense, Flatten, Dropout, Conv2D, MaxPooling2D, Input, InputLayer
from keras.layers import BatchNormalization
from keras.models import Model
from keras.callbacks import EarlyStopping
import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.metrics import confusion_matrix, roc_curve, auc, classification_report, precision_recall_curve, accuracy_score
from keras.utils import to_categorical

train_data = pd.read_csv('train.csv')
test_data = pd.read_csv('test.csv')

le = LabelEncoder()
train_data['Finish'] = le.fit_transform(train_data['Finish'])
test_data['Finish'] = le.transform(test_data['Finish'])

train_data = (train_data - train_data.min()) / (train_data.max() - train_data.min())
test_data = (test_data - test_data.min()) / (test_data.max() - test_data.min())

X_train = train_data.drop('Finish', axis=1)
Y_train = to_categorical(train_data['Finish'])
X_test = test_data.drop('Finish', axis=1)
Y_test = to_categorical(test_data['Finish'])

model = Sequential()
model.add(Dense(64, activation='relu', input_dim=11))
model.add(Dense(32, activation='relu'))
model.add(Dense(16, activation='relu'))
model.add(Dense(2, activation='sigmoid'))

model.compile(optimizer='adam', loss='categorical_crossentropy', metrics=['accuracy'])

early_stopping = EarlyStopping(
    monitor='accuracy',  
    patience=10,  
    mode='max'  
)

model.fit(X_train, Y_train, epochs=100, batch_size=16, callbacks=[early_stopping])

score = model.evaluate(X_test, Y_test, verbose=0)
print('Потери:', score[0])
print('Точность:', score[1])

predictions = model.predict(X_test)

# model.save('my_model.h5')

score = model.evaluate(X_test, Y_test, verbose=0)
print('Потери:', score[0])
print('Точность:', score[1])

predictions = np.argmax(predictions, axis=1)

pos = pd.read_csv("posx.csv")
pos['Finish'] = predictions
pos = pos.query("Finish == 1")
pos_array = pos.iloc[:, 0:10].values


value_counts = {}

for value in pos_array.flatten():
    if value in value_counts:
        value_counts[value] += 1
    else:
        value_counts[value] = 1

for value, count in value_counts.items():
    value_counts[value] = (count / len(pos_array.flatten())) * 100
for value, count in value_counts.items():
    value_counts[value] = round(count)

df = pd.DataFrame.from_dict(value_counts, orient="index")
df = df.T
df.to_csv("pers.csv", index=False)

