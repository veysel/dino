import requests
import json
import datetime

print("start")

http_time_list = []
json_time_list = []

for i in range(100):
    http_date_first = datetime.datetime.now()
    response = requests.get('https://raw.githubusercontent.com/tsopenteam/gundem/master/gundem.json')
    http_date_last = datetime.datetime.now()

    json_date_first = datetime.datetime.now()
    result = json.loads(response.text)
    json_date_last = datetime.datetime.now()

    http_time_list.append((http_date_last - http_date_first).total_seconds())
    json_time_list.append((json_date_last - json_date_first).total_seconds())

    print(f"{i+1} Http : {http_time_list[i]}")
    print(f"{i+1} Json : {json_time_list[i]}")

print("\nRESULT FOR PYTHON (seconds)")
print(f"Http First Request Time : {http_time_list[0]}")
print(f"Json First Parse Time : {json_time_list[0]}")
print("\n")
print(f"Http Average Request Time : {sum(http_time_list) / len(http_time_list)}")
print(f"Json Average Parse Time : {sum(json_time_list) / len(json_time_list)}")

print("end")