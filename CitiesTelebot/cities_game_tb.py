import telebot
from random import randint
import json
import requests

def save():
    """Функция для записи уже использованных названий городов в файл"""
    with open("used_cities.json","w",encoding="utf-8") as fh:
        fh.write(json.dumps(used_cities,ensure_ascii=False))

def load():
    global cities
    global used_cities
    """Функция для загрузки полного списка городов и списка уже использованных городов"""
    with open("cities.json","r",encoding="utf-8") as fh:
        cities=json.load(fh)    
    with open("used_cities.json","r",encoding="utf-8") as fh:
        used_cities=json.load(fh)        

def get_city(city):
    """Функция для выбора нового названия города"""
    last_letter = city.replace("ь","").upper()[-1]
    current_cities = []
    for city in cities:
        if city[0] == last_letter and city not in used_cities:
            current_cities.append(city)
    if len(current_cities) == 0:
        return None
    else:            
        return current_cities[randint(0, len(current_cities) - 1)]        

def right_choosen(old_city, new_city):
    """Функция, определяющая, правильно ли выбран город"""
    if old_city == "":
        return True
    old_city_last_letter = old_city.replace("ь","").replace("й","и").upper()[-1]
    new_city_first_letter = new_city[0]
    return old_city_last_letter == new_city_first_letter

with open('token.config') as f:
    API_TOKEN = f.read()
bot = telebot.TeleBot(API_TOKEN)

@bot.message_handler()
def test_city(message):
    global user_city
    global bot_city
    global used_cities
    user_city = message.text
    if user_city in cities:
        # Названный пользователем город существует
        # Проверяем, не использовался ли он ранее
        if user_city in used_cities:
            #  такой город уже называли
            bot.send_message(message.chat.id,"Вы уже называли этот город")
        else:
            # Такой город не называли
            # Проверяем, правильно ли пользователь выбрал город
            if right_choosen(bot_city, user_city):
                # Город выбран пользователем правильно
                bot_city = get_city(user_city)
                bot.send_message(message.chat.id, bot_city)
                used_cities.append(user_city)
                used_cities.append(bot_city)
            else:
                # Город выбран пользователем неправильно
                bot_city_last_letter = bot_city.replace("ь","").upper()[-1]
                bot.send_message(message.chat.id, f"Город должен начинаться на букву {bot_city_last_letter}")    
    else:
        # Названный пользователем город не существует
        bot.send_message(message.chat.id,"В России такого города нет")

cities = []
used_cities = []
user_city = ""
bot_city = ""

load()

bot.infinity_polling()