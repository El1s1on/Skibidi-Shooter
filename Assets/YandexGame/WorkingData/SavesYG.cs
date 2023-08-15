
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Настройки
        public bool musicEnabled;
        public bool ppEnabled;
        public float volumeValue;
        public float sensitivityValue;

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
            musicEnabled = true;
            ppEnabled = true;
            volumeValue = 1f;
            sensitivityValue = 1f;
        }
    }
}
