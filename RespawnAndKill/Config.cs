using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace RespawnAndKill
{
    public class Config : IConfig
    {
        // General
        [Description("Включить или выключить плагин.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Включить или выключить режим отладки (вывод подробной информации в консоль).")]
        public bool Debug { get; set; } = false;
        
        [Description("Включить команду .respawn?")]
        public bool IsRespawnCommandEnabled { get; set; } = true;

        [Description("Включить команду .kill?")]
        public bool IsKillCommandEnabled { get; set; } = true;

        // RSP
        [Description("Максимальное время (в секундах) с начала раунда, в течение которого можно использовать команду .respawn. Установите 0, чтобы отключить ограничение.")]
        public float RespawnTimeLimit { get; set; } = 300f;
        
        [Description("RSP: Включить функцию 'Опасное возрождение'? Если true, игрок получит ускорение, если возродится рядом с SCP.")]
        public bool IsDangerSpawnEnabled { get; set; } = true;

        [Description("RSP: Текст подсказки, который увидит игрок при опасном возрождении.")]
        public string DangerSpawnHint { get; set; } = "<color=red>Вы заспавнились в опасном месте. Бегите!</color>";

        [Description("RSP: Продолжительность (в секундах) эффекта ускорения при опасном возрождении.")]
        public float DangerSpawnBoostDuration { get; set; } = 10f;

        [Description("RSP: Интенсивность эффекта ускорения (в процентах). 20 = 20% ускорения.")]
        public byte DangerSpawnBoostIntensity { get; set; } = 60;
        
        [Description("RSP: Время ожидания (в секундах) перед повторным использованием команды .respawn.")]
        public float RespawnCooldown { get; set; } = 30f;

        [Description("RSP: Шанс (в процентах) возродиться как Класс-D.")]
        public int DClassSpawnChance { get; set; } = 70;

        [Description("RSP: Шанс (в процентах) возродиться как Ученый. Примечание: сумма шансов с DClass не обязательно должна быть 100.")]
        public int ScientistSpawnChance { get; set; } = 30;

        // KILL
        [Description("KILL: Максимальное количество символов для пользовательской причины смерти.")]
        public int KillReasonCharLimit { get; set; } = 100;

        [Description("KILL: Список случайных причин смерти, если причина не указана.")]
        public List<string> RandomKillReasons { get; set; } = new List<string>
        {
            "Решил проверить, есть ли урон от падения.",
            "Заблудился в мыслях.",
            "Увидел страшный сон.",
            "Поскользнулся на банановой кожуре.",
            "Попытался обнять SCP-173."
        };
    }
}
