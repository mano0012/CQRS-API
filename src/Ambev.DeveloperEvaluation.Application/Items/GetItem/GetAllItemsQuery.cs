﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Items.GetItem;

public class GetAllItemsQuery : IRequest<IEnumerable<GetItemResult>>
{
}
