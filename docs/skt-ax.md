working on ...

> https://docs.vllm.ai/en/latest/serving/openai_compatible_server.html#extra-parameters_1

```
git clone https://github.com/vllm-project/vllm.git          
cd vllm                                
uv pip install -r requirements/cpu.txt
uv pip install -e .
```

```
vllm serve skt/A.X-4.0-Light --max-model-len 2048
```