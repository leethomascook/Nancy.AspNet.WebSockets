﻿using System;
using System.Linq;

namespace Nancy.AspNet.WebSockets
{
    public abstract class WebSocketNancyModule : NancyModule
    {
        protected WebSocketNancyModule()
        {
        }

        protected WebSocketNancyModule(string path)
            : base(path)
        {
        }

        public WsRouteBuilder WebSocket
        {
            get { return new WsRouteBuilder(this); }
        }

        public class WsRouteBuilder
        {
            private readonly NancyModule _parentModule;
            private readonly RouteBuilder _rb;

            public WsRouteBuilder(NancyModule parentModule)
            {
                _parentModule = parentModule;
                _rb = new RouteBuilder("GET", parentModule);
            }

            public Func<dynamic, IWebSocketHandler> this[string path]
            {
                set { _rb[path] = Wrap(value); }
            }

            public Func<dynamic, IWebSocketHandler> this[string path, Func<NancyContext, bool> condition]
            {
                set { _rb[path, condition] = Wrap(value); }
            }

            private Func<dynamic, dynamic> Wrap(Func<dynamic, IWebSocketHandler> fun)
            {
                return d =>
                {
                    var wsr = _parentModule.Request.Headers[Constants.WebsocketHeaderMarker].FirstOrDefault();
                    if (wsr == null)
                    {
                        return HttpStatusCode.NotFound;
                    }
                    var ret = fun(d);
                    return new WebSocketHandlerWrapperResponse(ret);
                };
            }
        }
    }
}