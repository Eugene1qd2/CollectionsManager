using CollectionManager.Data.Interfaces;
using CollectionManager.Data.Repositories;
using CollectionManager.Models.Collection;
using CollectionManager.Models.CollectionItem;
using CollectionManager.Models.Tag;
using CollectionManager.Services.Interfaces;

namespace CollectionManager.Services
{
    public class TagService : ITagService
    {
        ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
            //string[] strings ="ведомство\r\nбамбук\r\nгрунтовка\r\nангел\r\nмассажист\r\nавтовышка\r\nпарафин\r\nмалыш\r\nземлянин\r\nконечность\r\nизгородь\r\nнасос\r\nберет\r\nвоз\r\nокно\r\nаквалангист\r\nкосточка\r\nлейка\r\nперила\r\nкарась\r\nцинк\r\nэльф\r\nчулок\r\nазбука\r\nвремя\r\nелей\r\nворон\r\nжилье\r\nкожура\r\nигра\r\nколея\r\nафрика\r\nинструмент\r\nкоза\r\nпрямоугольник\r\nперрон\r\nбатрак\r\nрека\r\nдуб\r\nасбоцемент\r\nкровать\r\nжелудь\r\nмешочек\r\nмешок\r\nавтопокрышка\r\nбухта\r\nкино\r\nкатапульта\r\nвдовец\r\nКуба\r\nаргентина\r\nблесна\r\nтент\r\nатлетика\r\nгонг\r\nмундир\r\nКамчатка\r\nдождь\r\nавтовесы\r\nпергамент\r\nкосилка\r\nбумага\r\nбор\r\nтабакерка\r\nтюбик\r\nмарка\r\nметан\r\nзамок\r\nвино\r\nпылесос\r\nдуховка\r\nпирамида\r\nКанзас\r\nкупальник\r\nбогиня\r\nвишня\r\nкапиталист\r\nбарон\r\nВатикан\r\nкуртка\r\nгребень\r\nжаба\r\nпарковка\r\nаммиак\r\nухо\r\nкомбайнер\r\nсело\r\nмарихуана\r\nпанталоны\r\nмарля\r\nклетчатка\r\nраствор\r\nкреветка\r\nжаворонок\r\nколыбель\r\nлипа\r\nкомпозитор\r\nшпала\r\nизверг\r\nморщина\r\nдиплом\r\nбиосфера\r\nбарыш\r\nромашка\r\nручей\r\nкорзина\r\nбожья коровка\r\nалыча\r\nшпалера\r\nободок\r\nавиабаза\r\nавтоколонна\r\nщетка\r\nремень\r\nвозница\r\nвестник\r\nгарнитура\r\nкоптилка\r\nавиамеханик\r\nдиван\r\nДон\r\nвуаль\r\nВолга\r\nлицо\r\nмасленка\r\nречка\r\nНеаполь\r\nкактус\r\nкашалот\r\nЕвпатория\r\nгазель\r\nМарс\r\nбурундук\r\nзаслонка\r\nмаринад\r\nжилец\r\nпринтер\r\nшофер\r\nПетербург\r\nглагол\r\nВьетнам\r\nсердце\r\nпономарь\r\nмышеловка\r\nпаспорт\r\nплато\r\nЛиверпуль\r\nграммофон\r\nалебастр\r\nвентилятор\r\nкуница\r\nгубернатор\r\nпруд\r\nперина\r\nдымоход\r\nбатог\r\nрог\r\nкорпус\r\nавстралиец\r\nмоль\r\nкопыто\r\nантибиотик\r\nпункт\r\nколодец\r\nбелладонна\r\nгортань\r\nдиктатор\r\nсамолет\r\nмашина\r\nкуча\r\nвермишель\r\nлента\r\nкрендель\r\nсундук\r\nзавиток\r\nбусинка\r\nвазон\r\nклапан\r\nфасад\r\nудав\r\nкомендант\r\nслон\r\nгантеля\r\nкупол\r\nпаук\r\nповидло\r\nуксус\r\nарктика\r\nкоробок\r\nнора\r\nладоши\r\nмак\r\nкабанчик\r\nбочка\r\nсазан\r\nкаюта\r\nмонумент\r\nклерк\r\nпластик\r\nбалка".Split("\r\n").ToArray() ;
            //foreach (string s in strings)
            //{
            //    _tagRepository.Create(new TagModel(s));
            //}    
            //сделать для 2 языков
        }

        public async Task Create(TagModel model)
        {
            if(model == null) throw new ArgumentNullException("model");
            await _tagRepository.Create(model);
        }

        public async Task CreateTags(List<TagModel> tags)
        {
            await _tagRepository.CraeteTags(tags);
        }

        public async Task DeleteById(string objId)
        {
            var tag=await _tagRepository.GetById(objId);
            await _tagRepository.Delete(tag);
        }

        public async Task<IEnumerable<TagModel>> GetAll()
        {
            return await _tagRepository.GetAll();
        }

        public async Task<TagModel> GetById(string objId)
        {
            return await _tagRepository.GetById(objId);
        }

        public async Task<IEnumerable<TagModel>> GetByItemId(string itemId)
        {
            return await _tagRepository.GetByItemId(itemId);
        }
    }
}
