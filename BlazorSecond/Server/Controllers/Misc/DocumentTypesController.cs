using AutoMapper;
using BlazorSecond.Server.MediatR.DocumentTypes;
using BlazorSecond.Shared.Model.DocumentTypes;
using BlazorSecond.Shared.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorSecond.Server.Controllers.Misc
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DocumentTypesController : BaseApiController<DocumentTypesController>
    {
        private readonly IMapper _mapper;

        public DocumentTypesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// 全てのデータを取得
        /// </summary>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.DocumentTypes.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Result<List<GetAllDocumentTypesResponse>>が返ってくる
            var documentTypes = await _mediator.Send(new GetAllDocumentTypesQuery());

            // Result<List<DocumentTypeRow>>に変換
            var documentTypess = _mapper.Map<Result<List<GetAllDocumentTypesResponse>>, Result<List<DocumentTypeDto>>>(documentTypes);

            return Ok(documentTypess);
        }

        /// <summary>
        /// IDを指定してデータを取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        //[Authorize(Policy = Permissions.DocumentTypes.View)]
        [HttpGet("{id}")]       // ここで"getById/{id}"のようにルーティングしても良い
        public async Task<IActionResult> GetById(int id)
        {
            var documentType = await _mediator.Send(new GetDocumentTypeByIdQuery { Id = id });
            return Ok(documentType);
        }

        /// <summary>
        /// 追加または更新を行う
        /// （追加と更新を分ける場合は片方をHttpPutにしてPutAsJsonAsyncで送るという手がある。）
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.DocumentTypes.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(DocumentTypeDto command)
        {
            // DocumentTypeRowとAddEditDocumentTypeCommandは同じ構造なので直接AddEditDocumentTypeCommandで受けても良いが、そうでない画面とコードを統一するためマッピングする
            return Ok(await _mediator.Send(_mapper.Map<AddEditDocumentTypeCommand>(command)));
        }

        /// <summary>
        /// 指定したIDを削除する
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.DocumentTypes.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteDocumentTypeCommand { Id = id }));
        }

        ///// <summary>
        ///// Search Document Types and Export to Excel
        ///// </summary>
        ///// <param name="searchString"></param>
        ///// <returns></returns>
        ////[Authorize(Policy = Permissions.DocumentTypes.Export)]
        //[HttpGet("export")]
        //public async Task<IActionResult> Export(string searchString = "")
        //{
        //    return Ok(await _mediator.Send(new ExportDocumentTypesQuery(searchString)));
        //}
    }
}